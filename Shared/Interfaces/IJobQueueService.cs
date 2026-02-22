using System.Collections.Generic;
using System.Threading.Tasks;
using SubTube.Shared.Models;

namespace SubTube.Shared.Interfaces
{
    public interface IJobQueueService
    {
        Task<SubTubeJob> EnqueueJobAsync(SubTubeJob job);
        Task<List<SubTubeJob>> GetUserJobsAsync(int userId);
        Task<SubTubeJob> GetJobStatusAsync(int jobId);
        Task UpdateJobStatusAsync(int jobId, JobStatus status, string error = null);
    }
}
