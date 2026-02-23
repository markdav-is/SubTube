using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using SubTube.Module.Pricing.Repository;
using SubTube.Module.Pricing.Services;

namespace SubTube.Module.Pricing.Startup
{
    public class PricingServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPricingService, ServerPricingService>();
            services.AddDbContextFactory<PricingContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
