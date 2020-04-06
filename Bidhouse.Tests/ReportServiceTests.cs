using Bidhouse.Models;
using Bidhouse.Services.Posts;
using Bidhouse.Services.Reports;
using Bidhouse.Tests.Factories;
using Bidhouse.ViewModels.PostModels;
using Bidhouse.ViewModels.ReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bidhouse.Tests
{
    public class ReportServiceTests
    {
        public ServiceFactory services { get; set; }
        public IReportService reportService { get; set; }

        public string postId { get; set; }
        public string userId { get; set; }

        public ReportServiceTests()
        {
            this.services = new ServiceFactory();
            this.reportService = new ReportService(this.services.Context);
            var postService = new PostService(this.services.Context);

            var reportedUser = new User()
            {
                UserName = "ReportedUser"
            };

            var reportingUser = new User()
            {
                UserName = "ReportingUser"
            };

            this.services.UserManager.CreateAsync(reportedUser, "123456789").Wait();
            this.services.UserManager.CreateAsync(reportingUser, "123456789").Wait();

            var input = new CreatePostInputModel()
            {
                Name = "Test",
                Price = 2,
                Category = Category.Design,
                Time = DateTime.Now,
                Description = " afkjafskajfakjfsaksfjgaksfjgafksjgafkj",
                Location = "Sofia"
            };

            this.userId = reportingUser.Id;
            this.postId = postService.CreatePost(input, reportedUser.Id).Result;

        }

        [Fact]
        public async Task AddReportShouldCreateReport()
        {
            var input = new ReportInputModel()
            {
                Description = "Description",
                PostId = this.postId,
                ReportType = ReportType.Crime
            };

            var result = await this.reportService.AddReport(userId, input);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteReportShouldDeleteReport()
        {
            var input = new ReportInputModel()
            {
                Description = "Description",
                PostId = this.postId,
                ReportType = ReportType.Crime
            };

            var reportId = await this.reportService.AddReport(userId, input);

            var result = await this.reportService.DeleteReport(reportId);

            Assert.True(result == "Successfully removed report");
        }

        [Fact]
        public async Task GetReportShouldReturnReport()
        {
            var input = new ReportInputModel()
            {
                Description = "Description",
                PostId = this.postId,
                ReportType = ReportType.Crime
            };

            var reportId = await this.reportService.AddReport(userId, input);

            var result = await this.reportService.GetReport(reportId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetReportsShouldReturnReports()
        {
            var input = new ReportInputModel()
            {
                Description = "Description",
                PostId = this.postId,
                ReportType = ReportType.Crime
            };

            await this.reportService.AddReport(userId, input);

            var result = await this.reportService.GetReports();

            Assert.NotNull(result);
        }

    }
}
