using Oqtane.Interfaces;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Repository.Databases.Interfaces;
using SubTube.Server.Repository;

namespace SubTube.Server.Manager
{
    public class SubTubeManager : MigratableModuleBase, IInstallable
    {
        private readonly IDBContextDependencies _DBContextDependencies;

        public SubTubeManager(IDBContextDependencies DBContextDependencies)
        {
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new SubTubeDBContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new SubTubeDBContext(_DBContextDependencies), tenant, MigrationType.Down);
        }
    }
}
