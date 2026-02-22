using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace SubTube.Server.Migrations.EntityBuilders
{
    public class SubTubeJobEntityBuilder : AuditableBaseEntityBuilder<SubTubeJobEntityBuilder>
    {
        private const string _entityTableName = "SubTubeJobs";
        private readonly PrimaryKey<SubTubeJobEntityBuilder> _primaryKey =
            new("PK_SubTubeJobs", x => x.JobId);

        public SubTubeJobEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database)
            : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
        }

        protected override SubTubeJobEntityBuilder BuildTable(ColumnsBuilder table)
        {
            JobId = AddAutoIncrementColumn(table, "JobId");
            UserId = AddIntegerColumn(table, "UserId");
            SubstackPostUrl = AddMaxStringColumn(table, "SubstackPostUrl");
            SubstackPostTitle = AddMaxStringColumn(table, "SubstackPostTitle");
            SubstackPostContent = AddMaxStringColumn(table, "SubstackPostContent");
            Status = AddIntegerColumn(table, "Status");
            CompletedOn = AddDateTimeColumn(table, "CompletedOn", true);
            AudioBlobPath = AddMaxStringColumn(table, "AudioBlobPath", true);
            VideoBlobPath = AddMaxStringColumn(table, "VideoBlobPath", true);
            YouTubeVideoId = AddMaxStringColumn(table, "YouTubeVideoId", true);
            ErrorMessage = AddMaxStringColumn(table, "ErrorMessage", true);
            DurationSeconds = AddIntegerColumn(table, "DurationSeconds", true);
            AddAuditableColumns(table); // CreatedBy, CreatedOn, ModifiedBy, ModifiedOn from ModelBase
            return this;
        }

        public OperationBuilder<AddColumnOperation> JobId { get; set; }
        public OperationBuilder<AddColumnOperation> UserId { get; set; }
        public OperationBuilder<AddColumnOperation> SubstackPostUrl { get; set; }
        public OperationBuilder<AddColumnOperation> SubstackPostTitle { get; set; }
        public OperationBuilder<AddColumnOperation> SubstackPostContent { get; set; }
        public OperationBuilder<AddColumnOperation> Status { get; set; }
        public OperationBuilder<AddColumnOperation> CompletedOn { get; set; }
        public OperationBuilder<AddColumnOperation> AudioBlobPath { get; set; }
        public OperationBuilder<AddColumnOperation> VideoBlobPath { get; set; }
        public OperationBuilder<AddColumnOperation> YouTubeVideoId { get; set; }
        public OperationBuilder<AddColumnOperation> ErrorMessage { get; set; }
        public OperationBuilder<AddColumnOperation> DurationSeconds { get; set; }
    }
}
