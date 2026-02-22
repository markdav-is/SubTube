using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace SubTube.Shared.Models
{
    [Table("SubTubeJobs")]
    public class SubTubeJob : ModelBase
    {
        [Key]
        public int JobId { get; set; }

        public int UserId { get; set; }

        public string SubstackPostUrl { get; set; }

        public string SubstackPostTitle { get; set; }

        public string SubstackPostContent { get; set; }

        public JobStatus Status { get; set; }

        // CreatedOn inherited from ModelBase (IAuditable)
        public DateTime? CompletedOn { get; set; }

        public string AudioBlobPath { get; set; }

        public string VideoBlobPath { get; set; }

        public string YouTubeVideoId { get; set; }

        public string ErrorMessage { get; set; }

        public int? DurationSeconds { get; set; }
    }
}
