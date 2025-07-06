using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Identity
{
    public class ApppUserr : BaseEntity<string>
    {
        public string DisplayName { get; set; }
        public Students Student { get; set; }
        public Owners Owner { get; set; }
    }
}
