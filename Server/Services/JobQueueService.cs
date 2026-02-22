using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SubTube.Server.Repository;
using SubTube.Shared.Interfaces;
using SubTube.Shared.Models;

namespace SubTube.Server.Services
{
    public class JobQueueService : IJobQueueService
    {
        private readonly ISubTubeJobRepository _jobRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JobQueueService> _logger;

        public JobQueueService(
            ISubTubeJobRepository jobRepository,
            IConfiguration configuration,
            ILogger<JobQueueService> logger)
        {
            _jobRepository = jobRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<SubTubeJob> EnqueueJobAsync(SubTubeJob job)
        {
            job.Status = JobStatus.Queued;
            job.CreatedOn = DateTime.UtcNow;

            var saved = _jobRepository.AddJob(job);

            var connectionString = _configuration["SubTube:AzureStorageConnection"];
            var queueName = _configuration["SubTube:AzureQueueName"] ?? "subtube-jobs";

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    var queueClient = new QueueClient(connectionString, queueName,
                        new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
                    await queueClient.CreateIfNotExistsAsync();
                    var messageJson = JsonSerializer.Serialize(new { JobId = saved.JobId, UserId = saved.UserId });
                    await queueClient.SendMessageAsync(messageJson);
                    _logger.LogInformation("Enqueued job {JobId} to Azure Queue {QueueName}", saved.JobId, queueName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send job {JobId} to Azure Queue", saved.JobId);
                }
            }
            else
            {
                _logger.LogWarning("AzureStorageConnection not configured — job {JobId} saved to DB only", saved.JobId);
            }

            return saved;
        }

        public Task<List<SubTubeJob>> GetUserJobsAsync(int userId)
        {
            var jobs = _jobRepository.GetUserJobs(userId).ToList();
            return Task.FromResult(jobs);
        }

        public Task<SubTubeJob> GetJobStatusAsync(int jobId)
        {
            var job = _jobRepository.GetJob(jobId);
            return Task.FromResult(job);
        }

        public Task UpdateJobStatusAsync(int jobId, JobStatus status, string error = null)
        {
            var job = _jobRepository.GetJob(jobId);
            if (job == null) return Task.CompletedTask;

            job.Status = status;
            if (error != null)
                job.ErrorMessage = error;
            if (status == JobStatus.Complete || status == JobStatus.Failed)
                job.CompletedOn = DateTime.UtcNow;

            _jobRepository.UpdateJob(job);
            return Task.CompletedTask;
        }
    }
}
