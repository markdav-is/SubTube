using Oqtane.Models;
using Oqtane.Modules;

namespace SubTube.Module.Pricing
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Pricing",
            Description = "The Pricing Module",
            Version = "1.0.0",
            ServerManagerType = "SubTube.Module.Pricing.Manager.PricingManager, SubTube.Server.Oqtane"
        };
    }
}
