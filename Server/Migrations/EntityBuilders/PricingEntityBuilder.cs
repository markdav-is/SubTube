using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace SubTube.Module.Pricing.Migrations.EntityBuilders
{
    public class PricingEntityBuilder : AuditableBaseEntityBuilder<PricingEntityBuilder>
    {
        private const string _entityTableName = "SubTubePricing";
        private readonly PrimaryKey<PricingEntityBuilder> _primaryKey = new("PK_SubTubePricing", x => x.PricingId);
        private readonly ForeignKey<PricingEntityBuilder> _moduleForeignKey = new("FK_SubTubePricing_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public PricingEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override PricingEntityBuilder BuildTable(ColumnsBuilder table)
        {
            PricingId = AddAutoIncrementColumn(table,"PricingId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> PricingId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
