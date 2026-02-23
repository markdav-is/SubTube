using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace SubTube.Module.Pricing.Repository
{
    public class PricingContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Pricing> Pricing { get; set; }

        public PricingContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Pricing>().ToTable(ActiveDatabase.RewriteName("SubTubePricing"));
        }
    }
}
