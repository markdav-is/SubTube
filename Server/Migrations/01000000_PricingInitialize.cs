using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using SubTube.Module.Pricing.Migrations.EntityBuilders;
using SubTube.Module.Pricing.Repository;

namespace SubTube.Module.Pricing.Migrations
{
    [DbContext(typeof(PricingContext))]
    [Migration("SubTube.Module.Pricing.01.00.00.00")]
    public class PricingInitialize : MultiDatabaseMigration
    {
        public PricingInitialize(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new PricingEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new PricingEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
