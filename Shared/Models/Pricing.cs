using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace SubTube.Module.Pricing.Models
{
    [Table("SubTubePricing")]
    public class Pricing : ModelBase
    {
        [Key]
        public int PricingId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
    }
}
