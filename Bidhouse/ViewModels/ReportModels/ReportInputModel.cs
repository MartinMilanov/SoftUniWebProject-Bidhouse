using Bidhouse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.ViewModels.ReportModels
{
    public class ReportInputModel
    {
        [Required(ErrorMessage = "Please provide the post's id")]
        public string PostId { get; set; }
        [Required(ErrorMessage = "Please fill out your description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose a report type")]
        public ReportType ReportType { get; set; }

    }
}
