using System.Collections.Generic;
using System.Threading.Tasks;
using SubTube.Shared.Models;

namespace SubTube.Shared.Interfaces
{
    public interface ISubstackService
    {
        Task<List<SubstackPost>> ParseFeedAsync(string substackUrl);
        Task<List<SubstackPost>> CheckForNewPostsAsync(int userId);
    }
}
