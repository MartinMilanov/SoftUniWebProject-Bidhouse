using System;

namespace Bidhouse.Models
{
    public class Report
    {
        public Report()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string ReporterId { get; set; }
        public string ReportedPostId { get; set; }
        public Post ReportedPost { get; set; }
        public User Reporter { get; set; }
        public string Description { get; set; }
        public ReportType ReportType { get; set; }
    }
}