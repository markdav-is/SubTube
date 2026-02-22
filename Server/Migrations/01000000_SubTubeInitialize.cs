using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using SubTube.Server.Migrations.EntityBuilders;
using SubTube.Server.Repository;

namespace SubTube.Server.Migrations
{
    [DbContext(typeof(SubTubeDBContext))]
    [Migration("SubTube.Server.01.00.00.00")]
    public class SubTubeInitialize : MultiDatabaseMigration
    {
        public SubTubeInitialize(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var jobBuilder = new SubTubeJobEntityBuilder(migrationBuilder, ActiveDatabase);
            jobBuilder.Create();

            var feedBuilder = new SubstackFeedEntityBuilder(migrationBuilder, ActiveDatabase);
            feedBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var jobBuilder = new SubTubeJobEntityBuilder(migrationBuilder, ActiveDatabase);
            jobBuilder.Drop();

            var feedBuilder = new SubstackFeedEntityBuilder(migrationBuilder, ActiveDatabase);
            feedBuilder.Drop();
        }
    }
}
