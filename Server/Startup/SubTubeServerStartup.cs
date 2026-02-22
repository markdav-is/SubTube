using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using SubTube.Server.Repository;
using SubTube.Server.Services;
using SubTube.Shared.Interfaces;

namespace SubTube.Server.Startup
{
    public class SubTubeServerStartup : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ISubstackService, SubstackService>();
            services.AddScoped<IJobQueueService, JobQueueService>();
            services.AddScoped<ISubTubeJobRepository, SubTubeJobRepository>();
            services.AddScoped<ISubstackFeedRepository, SubstackFeedRepository>();
            services.AddDbContextFactory<SubTubeDBContext>(opt => { }, ServiceLifetime.Transient);
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }
    }
}
