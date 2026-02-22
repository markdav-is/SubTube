using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oqtane.Modules;
using SubTube.Shared.Models;

namespace SubTube.Server.Repository
{
    public class SubTubeJobRepository : ISubTubeJobRepository, ITransientService
    {
        private readonly IDbContextFactory<SubTubeDBContext> _factory;

        public SubTubeJobRepository(IDbContextFactory<SubTubeDBContext> factory)
        {
            _factory = factory;
        }

        public SubTubeJob AddJob(SubTubeJob job)
        {
            using var db = _factory.CreateDbContext();
            db.SubTubeJobs.Add(job);
            db.SaveChanges();
            return job;
        }

        public SubTubeJob GetJob(int jobId)
        {
            using var db = _factory.CreateDbContext();
            return db.SubTubeJobs.AsNoTracking().FirstOrDefault(j => j.JobId == jobId);
        }

        public IEnumerable<SubTubeJob> GetUserJobs(int userId)
        {
            using var db = _factory.CreateDbContext();
            return db.SubTubeJobs.AsNoTracking()
                .Where(j => j.UserId == userId)
                .OrderByDescending(j => j.CreatedOn)
                .ToList();
        }

        public SubTubeJob UpdateJob(SubTubeJob job)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
            return job;
        }
    }
}
