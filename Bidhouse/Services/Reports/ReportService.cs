using Bidhouse.Models;
using Bidhouse.ViewModels.ReportModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext db;

        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<string> AddReport(string id, ReportInputModel input)
        {
            var result = this.db.Posts.Any(x => x.Id == input.PostId);

            if (result == false)
            {
                return "Report not found";
            }

            var report = new Report
            {
                Description = input.Description,
                ReportedPost = this.db.Posts.FirstOrDefault(x => x.Id == input.PostId),
                Reporter = this.db.Users.FirstOrDefault(x => x.Id == id),
                ReportType = input.ReportType
            };

            await this.db.Reports.AddAsync(report);
            await this.db.SaveChangesAsync();

            return report.Id;
        }

        public async Task<string> DeleteReport(string id)
        {
            if (this.db.Reports.Any(x => x.Id == id))
            {
                this.db.Reports.Remove(await this.db.Reports.FirstOrDefaultAsync(x => x.Id == id));
                await this.db.SaveChangesAsync();
                return "Successfully removed report";
            }

            return "Could not remove report";
            
        }

        public async Task<ReportDetailViewModel> GetReport(string id)
        {
            if (await this.db.Reports.AnyAsync(x=>x.Id == id) == false)
            {
                return null;
            }

            var query = await this.db.Reports
                .Include(x=>x.Reporter)
                .Include(x=>x.ReportedPost)
                .FirstOrDefaultAsync(x => x.Id == id);

            var report = new ReportDetailViewModel
            {
                Id = query.Id,
                Description = query.Description,
                ReportType = query.ReportType.ToString(),
                PostId = query.ReportedPostId,
                PostName = query.ReportedPost.Name,
                ReporterId = query.ReporterId,
                ReporterName = query.Reporter.UserName,
                ReporterImageUrl = query.Reporter.ImageUrl
            };

            return report;


        }

        public async Task<ICollection<ReportListViewModel>> GetReports()
        {
            var reports = await this.db.Reports.Include(x => x.Reporter)
                .Select(x => 
                 new ReportListViewModel 
                { 
                    Id = x.Id,
                    ReportType = x.ReportType.ToString(),
                    ReporterName = x.Reporter.UserName
                }).ToListAsync();

            return reports;

        }
    }
}
