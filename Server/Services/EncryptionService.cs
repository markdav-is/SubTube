using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using SubTube.Shared.Interfaces;

namespace SubTube.Server.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration configuration)
        {
            var rawKey = configuration["SubTube:EncryptionKey"] ?? string.Empty;
            var keyBytes = Encoding.UTF8.GetBytes(rawKey);
            _key = new byte[32];
            Buffer.BlockCopy(keyBytes, 0, _key, 0, Math.Min(keyBytes.Length, 32));
        }

        public string EncryptString(string plaintext)
        {
            // Encrypt using AES-GCM to provide confidentiality and integrity.
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext ?? string.Empty);

            // Recommended nonce size for GCM is 12 bytes.
            byte[] nonce = new byte[12];
            RandomNumberGenerator.Fill(nonce);

            byte[] ciphertext = new byte[plaintextBytes.Length];
            byte[] tag = new byte[16]; // 128-bit authentication tag

            using (var aesGcm = new AesGcm(_key))
            {
                aesGcm.Encrypt(nonce, plaintextBytes, ciphertext, tag);
            }

            // Combine nonce + tag + ciphertext for storage/transmission.
            byte[] combined = new byte[nonce.Length + tag.Length + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, combined, 0, nonce.Length);
            Buffer.BlockCopy(tag, 0, combined, nonce.Length, tag.Length);
            Buffer.BlockCopy(ciphertext, 0, combined, nonce.Length + tag.Length, ciphertext.Length);

            return Convert.ToBase64String(combined);
        }

        public string DecryptString(string ciphertext)
        {
            var combined = Convert.FromBase64String(ciphertext);

            // combined = nonce (12) || tag (16) || ciphertext
            const int nonceSize = 12;
            const int tagSize = 16;

            if (combined.Length < nonceSize + tagSize)
            {
                throw new CryptographicException("Ciphertext is too short.");
            }

            byte[] nonce = new byte[nonceSize];
            byte[] tag = new byte[tagSize];
            byte[] actualCiphertext = new byte[combined.Length - nonceSize - tagSize];

            Buffer.BlockCopy(combined, 0, nonce, 0, nonceSize);
            Buffer.BlockCopy(combined, nonceSize, tag, 0, tagSize);
            Buffer.BlockCopy(combined, nonceSize + tagSize, actualCiphertext, 0, actualCiphertext.Length);

            byte[] plaintextBytes = new byte[actualCiphertext.Length];

            using (var aesGcm = new AesGcm(_key))
            {
                // Will throw CryptographicException if authentication fails (tampering detected).
                aesGcm.Decrypt(nonce, actualCiphertext, tag, plaintextBytes);
            }

            return Encoding.UTF8.GetString(plaintextBytes);
        }
    }
}
