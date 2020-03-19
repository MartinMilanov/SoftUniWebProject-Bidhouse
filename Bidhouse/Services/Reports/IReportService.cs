using Bidhouse.ViewModels.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Reports
{
    public interface IReportService
    {
        public Task<string> AddReport(string id , ReportInputModel input);
        public Task<string> DeleteReport(string id);
        public Task<ICollection<ReportListViewModel>> GetReports();
        public Task<ReportDetailViewModel> GetReport(string id);
    }
}
