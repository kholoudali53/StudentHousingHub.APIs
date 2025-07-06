using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Reports
{
    public class CreateReportDto
    {
        [Required(ErrorMessage = "Problem description is required")]
        [StringLength(1000, ErrorMessage = "Problem description cannot exceed 1000 characters")]
        public string Problem { get; set; }
    }
}
