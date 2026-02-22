using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace SubTube.Shared.Models
{
    [Table("SubstackFeeds")]
    public class SubstackFeed : ModelBase
    {
        [Key]
        public int FeedId { get; set; }

        public int UserId { get; set; }

        public string SubstackUrl { get; set; }

        public DateTime? LastCheckedOn { get; set; }

        public string LastPostGuid { get; set; }
    }
}
