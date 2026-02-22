using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oqtane.Repository;
using SubTube.Shared.Interfaces;
using SubTube.Shared.Models;

namespace SubTube.Server.Services
{
    public class RssPollingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RssPollingService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(6);

        public RssPollingService(IServiceScopeFactory scopeFactory, ILogger<RssPollingService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RssPollingService started. Polling interval: {Interval}", _interval);

            while (!stoppingToken.IsCancellationRequested)
            {
                await PollAllUsersAsync();
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task PollAllUsersAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var substackService = scope.ServiceProvider.GetRequiredService<ISubstackService>();
                var settingRepository = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
                var siteRepository = scope.ServiceProvider.GetRequiredService<ISiteRepository>();

                var sites = siteRepository.GetSites();
                foreach (var site in sites)
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                    var users = userRepository.GetUsers(site.SiteId);

                    foreach (var user in users)
                    {
                        try
                        {
                            var setting = settingRepository.GetSetting("User", user.UserId, UserProfileKeys.SubstackUrl);
                            if (setting == null || string.IsNullOrEmpty(setting.SettingValue))
                                continue;

                            var newPosts = await substackService.CheckForNewPostsAsync(user.UserId);
                            _logger.LogInformation(
                                "User {UserId}: found {Count} new Substack post(s)",
                                user.UserId, newPosts.Count);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error checking Substack for user {UserId}", user.UserId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RssPollingService poll cycle");
            }
        }
    }
}
