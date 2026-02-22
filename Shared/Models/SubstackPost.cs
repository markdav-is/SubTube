using System;

namespace SubTube.Shared.Models
{
    /// <summary>
    /// DTO only — not persisted to database
    /// </summary>
    public class SubstackPost
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Guid { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
