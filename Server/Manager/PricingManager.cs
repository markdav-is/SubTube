using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using SubTube.Module.Pricing.Repository;
using System.Threading.Tasks;

namespace SubTube.Module.Pricing.Manager
{
    public class PricingManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IPricingRepository _PricingRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public PricingManager(IPricingRepository PricingRepository, IDBContextDependencies DBContextDependencies)
        {
            _PricingRepository = PricingRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new PricingContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new PricingContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Pricing> Pricings = _PricingRepository.GetPricings(module.ModuleId).ToList();
            if (Pricings != null)
            {
                content = JsonSerializer.Serialize(Pricings);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Pricing> Pricings = null;
            if (!string.IsNullOrEmpty(content))
            {
                Pricings = JsonSerializer.Deserialize<List<Models.Pricing>>(content);
            }
            if (Pricings != null)
            {
                foreach(var Pricing in Pricings)
                {
                    _PricingRepository.AddPricing(new Models.Pricing { ModuleId = module.ModuleId, Name = Pricing.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Pricing in _PricingRepository.GetPricings(pageModule.ModuleId))
           {
               if (Pricing.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "SubTubePricing",
                       EntityId = Pricing.PricingId.ToString(),
                       Title = Pricing.Name,
                       Body = Pricing.Name,
                       ContentModifiedBy = Pricing.ModifiedBy,
                       ContentModifiedOn = Pricing.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
