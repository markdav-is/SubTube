using Microsoft.EntityFrameworkCore;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Repository.Databases.Interfaces;
using SubTube.Shared.Models;

namespace SubTube.Server.Repository
{
    public class SubTubeDBContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<SubTubeJob> SubTubeJobs { get; set; }
        public virtual DbSet<SubstackFeed> SubstackFeeds { get; set; }

        public SubTubeDBContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SubTubeJob>().ToTable(ActiveDatabase.RewriteName("SubTubeJobs"));
            builder.Entity<SubstackFeed>().ToTable(ActiveDatabase.RewriteName("SubstackFeeds"));
        }
    }
}
