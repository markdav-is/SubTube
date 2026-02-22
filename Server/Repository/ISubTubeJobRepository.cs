using System.Collections.Generic;
using SubTube.Shared.Models;

namespace SubTube.Server.Repository
{
    public interface ISubTubeJobRepository
    {
        SubTubeJob AddJob(SubTubeJob job);
        SubTubeJob GetJob(int jobId);
        IEnumerable<SubTubeJob> GetUserJobs(int userId);
        SubTubeJob UpdateJob(SubTubeJob job);
    }
}
