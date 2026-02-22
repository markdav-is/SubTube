using SubTube.Shared.Models;

namespace SubTube.Server.Repository
{
    public interface ISubstackFeedRepository
    {
        SubstackFeed GetFeedByUser(int userId);
        SubstackFeed AddFeed(SubstackFeed feed);
        SubstackFeed UpdateFeed(SubstackFeed feed);
    }
}
