using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Oqtane.Services;
using SubTube.Module.Pricing.Services;

namespace SubTube.Module.Pricing.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (!services.Any(s => s.ServiceType == typeof(IPricingService)))
            {
                services.AddScoped<IPricingService, PricingService>();
            }
        }
    }
}
