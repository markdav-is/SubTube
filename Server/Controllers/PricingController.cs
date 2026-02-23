using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using SubTube.Module.Pricing.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace SubTube.Module.Pricing.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class PricingController : ModuleControllerBase
    {
        private readonly IPricingService _PricingService;

        public PricingController(IPricingService PricingService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _PricingService = PricingService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.Pricing>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _PricingService.GetPricingsAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.Pricing> Get(int id, int moduleid)
        {
            Models.Pricing Pricing = await _PricingService.GetPricingAsync(id, moduleid);
            if (Pricing != null && IsAuthorizedEntityId(EntityNames.Module, Pricing.ModuleId))
            {
                return Pricing;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Get Attempt {PricingId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.Pricing> Post([FromBody] Models.Pricing Pricing)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Pricing.ModuleId))
            {
                Pricing = await _PricingService.AddPricingAsync(Pricing);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Post Attempt {Pricing}", Pricing);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Pricing = null;
            }
            return Pricing;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.Pricing> Put(int id, [FromBody] Models.Pricing Pricing)
        {
            if (ModelState.IsValid && Pricing.PricingId == id && IsAuthorizedEntityId(EntityNames.Module, Pricing.ModuleId))
            {
                Pricing = await _PricingService.UpdatePricingAsync(Pricing);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Put Attempt {Pricing}", Pricing);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Pricing = null;
            }
            return Pricing;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.Pricing Pricing = await _PricingService.GetPricingAsync(id, moduleid);
            if (Pricing != null && IsAuthorizedEntityId(EntityNames.Module, Pricing.ModuleId))
            {
                await _PricingService.DeletePricingAsync(id, Pricing.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Pricing Delete Attempt {PricingId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
