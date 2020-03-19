using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.ReportModels
{
    public class ReportDetailViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ReportType { get; set; }
        public string ReporterId { get; set; }
        public string ReporterName { get; set; }
        public string ReporterImageUrl { get; set; }
        public string PostId { get; set; }
        public string PostName { get; set; }
    }
}
