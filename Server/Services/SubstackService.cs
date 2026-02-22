using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SubTube.Server.Repository;
using SubTube.Shared.Interfaces;
using SubTube.Shared.Models;

namespace SubTube.Server.Services
{
    public class SubstackService : ISubstackService
    {
        private readonly ISubstackFeedRepository _feedRepository;
        private readonly ILogger<SubstackService> _logger;

        public SubstackService(ISubstackFeedRepository feedRepository, ILogger<SubstackService> logger)
        {
            _feedRepository = feedRepository;
            _logger = logger;
        }

        public async Task<List<SubstackPost>> ParseFeedAsync(string substackUrl)
        {
            var normalizedUrl = substackUrl
                .Replace("https://", string.Empty)
                .Replace("http://", string.Empty)
                .TrimEnd('/');

            var feedUrl = $"https://{normalizedUrl}/feed";
            _logger.LogInformation("Fetching Substack feed from {FeedUrl}", feedUrl);

            var feed = await FeedReader.ReadAsync(feedUrl);

            return feed.Items
                .Take(20)
                .Select(item => new SubstackPost
                {
                    Title = item.Title,
                    Url = item.Link,
                    Content = StripHtml(item.Content ?? item.Description ?? string.Empty),
                    Guid = item.Id,
                    PublishedDate = item.PublishingDate ?? DateTime.UtcNow
                })
                .ToList();
        }

        public async Task<List<SubstackPost>> CheckForNewPostsAsync(int userId)
        {
            var feed = _feedRepository.GetFeedByUser(userId);
            if (feed == null || string.IsNullOrEmpty(feed.SubstackUrl))
                return new List<SubstackPost>();

            var posts = await ParseFeedAsync(feed.SubstackUrl);
            if (posts.Count == 0)
                return new List<SubstackPost>();

            List<SubstackPost> newPosts;
            if (string.IsNullOrEmpty(feed.LastPostGuid))
            {
                newPosts = posts;
            }
            else
            {
                var lastSeenPost = posts.FirstOrDefault(p => p.Guid == feed.LastPostGuid);
                newPosts = lastSeenPost == null
                    ? posts
                    : posts.Where(p => p.PublishedDate > lastSeenPost.PublishedDate).ToList();
            }

            feed.LastCheckedOn = DateTime.UtcNow;
            if (posts.Count > 0)
            {
                var newestPost = posts.OrderByDescending(p => p.PublishedDate).First();
                feed.LastPostGuid = newestPost.Guid;
            }

            _feedRepository.UpdateFeed(feed);

            return newPosts;
        }

        private static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }
    }
}
