namespace SubTube.Shared.Interfaces
{
    public interface IEncryptionService
    {
        string EncryptString(string plaintext);
        string DecryptString(string ciphertext);
    }
}
