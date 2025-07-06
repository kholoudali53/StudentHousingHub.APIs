using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Reports
{
    public class ReportDto
    {
        public int id { get; set; }
        public string Problem { get; set; }
        public int StudentId { get; set; }
        public int OwnerId { get; set; }
        public int AdminId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
