using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using SubTube.Module.Pricing.Repository;

namespace SubTube.Module.Pricing.Services
{
    public class ServerPricingService : IPricingService
    {
        private readonly IPricingRepository _PricingRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerPricingService(IPricingRepository PricingRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _PricingRepository = PricingRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Pricing>> GetPricingsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_PricingRepository.GetPricings(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Pricing> GetPricingAsync(int PricingId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_PricingRepository.GetPricing(PricingId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Get Attempt {PricingId} {ModuleId}", PricingId, ModuleId);
                return null;
            }
        }

        public Task<Models.Pricing> AddPricingAsync(Models.Pricing Pricing)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Pricing.ModuleId, PermissionNames.Edit))
            {
                Pricing = _PricingRepository.AddPricing(Pricing);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Pricing Added {Pricing}", Pricing);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Add Attempt {Pricing}", Pricing);
                Pricing = null;
            }
            return Task.FromResult(Pricing);
        }

        public Task<Models.Pricing> UpdatePricingAsync(Models.Pricing Pricing)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Pricing.ModuleId, PermissionNames.Edit))
            {
                Pricing = _PricingRepository.UpdatePricing(Pricing);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Pricing Updated {Pricing}", Pricing);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Update Attempt {Pricing}", Pricing);
                Pricing = null;
            }
            return Task.FromResult(Pricing);
        }

        public Task DeletePricingAsync(int PricingId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _PricingRepository.DeletePricing(PricingId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Pricing Deleted {PricingId}", PricingId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Delete Attempt {PricingId} {ModuleId}", PricingId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
