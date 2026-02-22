using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace SubTube.Server.Migrations.EntityBuilders
{
    public class SubstackFeedEntityBuilder : AuditableBaseEntityBuilder<SubstackFeedEntityBuilder>
    {
        private const string _entityTableName = "SubstackFeeds";
        private readonly PrimaryKey<SubstackFeedEntityBuilder> _primaryKey =
            new("PK_SubstackFeeds", x => x.FeedId);

        public SubstackFeedEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database)
            : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
        }

        protected override SubstackFeedEntityBuilder BuildTable(ColumnsBuilder table)
        {
            FeedId = AddAutoIncrementColumn(table, "FeedId");
            UserId = AddIntegerColumn(table, "UserId");
            SubstackUrl = AddMaxStringColumn(table, "SubstackUrl");
            LastCheckedOn = AddDateTimeColumn(table, "LastCheckedOn", true);
            LastPostGuid = AddMaxStringColumn(table, "LastPostGuid", true);
            AddAuditableColumns(table); // CreatedBy, CreatedOn, ModifiedBy, ModifiedOn from ModelBase
            return this;
        }

        public OperationBuilder<AddColumnOperation> FeedId { get; set; }
        public OperationBuilder<AddColumnOperation> UserId { get; set; }
        public OperationBuilder<AddColumnOperation> SubstackUrl { get; set; }
        public OperationBuilder<AddColumnOperation> LastCheckedOn { get; set; }
        public OperationBuilder<AddColumnOperation> LastPostGuid { get; set; }
    }
}
