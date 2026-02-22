using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oqtane.Modules;
using SubTube.Shared.Models;

namespace SubTube.Server.Repository
{
    public class SubstackFeedRepository : ISubstackFeedRepository, ITransientService
    {
        private readonly IDbContextFactory<SubTubeDBContext> _factory;

        public SubstackFeedRepository(IDbContextFactory<SubTubeDBContext> factory)
        {
            _factory = factory;
        }

        public SubstackFeed GetFeedByUser(int userId)
        {
            using var db = _factory.CreateDbContext();
            return db.SubstackFeeds.AsNoTracking().FirstOrDefault(f => f.UserId == userId);
        }

        public SubstackFeed AddFeed(SubstackFeed feed)
        {
            using var db = _factory.CreateDbContext();
            db.SubstackFeeds.Add(feed);
            db.SaveChanges();
            return feed;
        }

        public SubstackFeed UpdateFeed(SubstackFeed feed)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(feed).State = EntityState.Modified;
            db.SaveChanges();
            return feed;
        }
    }
}
