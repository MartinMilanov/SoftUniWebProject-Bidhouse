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
        [Required]
        public string PostId { get; set; }
        [Required(ErrorMessage = "Please fill out your description")]
        public string Description { get; set; }
        [Required]
        public ReportType ReportType { get; set; }

    }
}
