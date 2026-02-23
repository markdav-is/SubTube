using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace SubTube.Module.Pricing.Repository
{
    public interface IPricingRepository
    {
        IEnumerable<Models.Pricing> GetPricings(int ModuleId);
        Models.Pricing GetPricing(int PricingId);
        Models.Pricing GetPricing(int PricingId, bool tracking);
        Models.Pricing AddPricing(Models.Pricing Pricing);
        Models.Pricing UpdatePricing(Models.Pricing Pricing);
        void DeletePricing(int PricingId);
    }

    public class PricingRepository : IPricingRepository, ITransientService
    {
        private readonly IDbContextFactory<PricingContext> _factory;

        public PricingRepository(IDbContextFactory<PricingContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Pricing> GetPricings(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Pricing.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Pricing GetPricing(int PricingId)
        {
            return GetPricing(PricingId, true);
        }

        public Models.Pricing GetPricing(int PricingId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Pricing.Find(PricingId);
            }
            else
            {
                return db.Pricing.AsNoTracking().FirstOrDefault(item => item.PricingId == PricingId);
            }
        }

        public Models.Pricing AddPricing(Models.Pricing Pricing)
        {
            using var db = _factory.CreateDbContext();
            db.Pricing.Add(Pricing);
            db.SaveChanges();
            return Pricing;
        }

        public Models.Pricing UpdatePricing(Models.Pricing Pricing)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Pricing).State = EntityState.Modified;
            db.SaveChanges();
            return Pricing;
        }

        public void DeletePricing(int PricingId)
        {
            using var db = _factory.CreateDbContext();
            Models.Pricing Pricing = db.Pricing.Find(PricingId);
            db.Pricing.Remove(Pricing);
            db.SaveChanges();
        }
    }
}
