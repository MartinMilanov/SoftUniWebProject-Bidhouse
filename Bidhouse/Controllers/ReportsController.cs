using Bidhouse.Services.Reports;
using Bidhouse.ViewModels.ReportModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bidhouse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController:ControllerBase
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Report([FromBody]ReportInputModel input)
        {
           
            var senderId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this.reportService.AddReport(senderId, input);

            if (result == "Post not found")
            {
                return BadRequest(result);
            }

            return Ok(new { result });
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> DeleteReport(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Enter an id");
            }

            var result = await this.reportService.DeleteReport(id);

            if (result == "Could not remove report")
            {
                return BadRequest(result);
            }

            return Ok(new { result });
        }
    
        [HttpGet("getReports")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ICollection<ReportListViewModel>>> GetReports()
        {
            var reports = await this.reportService.GetReports();

            return Ok(reports);
        }
    
        [HttpGet("getReport")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReportDetailViewModel>> GetReport(string id)
        {
            var report = await this.reportService.GetReport(id);
            if (report == null)
            {
                return BadRequest("Could not find report ! ");
            }

            return Ok(report);
        }
    }
}
