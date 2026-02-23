using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace SubTube.Module.Pricing.Services
{
    public interface IPricingService 
    {
        Task<List<Models.Pricing>> GetPricingsAsync(int ModuleId);

        Task<Models.Pricing> GetPricingAsync(int PricingId, int ModuleId);

        Task<Models.Pricing> AddPricingAsync(Models.Pricing Pricing);

        Task<Models.Pricing> UpdatePricingAsync(Models.Pricing Pricing);

        Task DeletePricingAsync(int PricingId, int ModuleId);
    }

    public class PricingService : ServiceBase, IPricingService
    {
        public PricingService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Pricing");

        public async Task<List<Models.Pricing>> GetPricingsAsync(int ModuleId)
        {
            List<Models.Pricing> Pricings = await GetJsonAsync<List<Models.Pricing>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Pricing>().ToList());
            return Pricings.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Pricing> GetPricingAsync(int PricingId, int ModuleId)
        {
            return await GetJsonAsync<Models.Pricing>(CreateAuthorizationPolicyUrl($"{Apiurl}/{PricingId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Pricing> AddPricingAsync(Models.Pricing Pricing)
        {
            return await PostJsonAsync<Models.Pricing>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Pricing.ModuleId), Pricing);
        }

        public async Task<Models.Pricing> UpdatePricingAsync(Models.Pricing Pricing)
        {
            return await PutJsonAsync<Models.Pricing>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Pricing.PricingId}", EntityNames.Module, Pricing.ModuleId), Pricing);
        }

        public async Task DeletePricingAsync(int PricingId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{PricingId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
