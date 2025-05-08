using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Reports : BaseEntity<int>
    {
        public string Problem { get; set; }


        // Foreign Key for Student
        public int StudentId { get; set; }
        public Students Student { get; set; } // Many-to-One with Student

        // Foreign Key for Owner
        public int OwnerId { get; set; }
        public Owners Owner { get; set; } // Many-to-One with Owner

        // Foreign Key for Admin (Optional, if the admin reviews reports)
        public int AdminId { get; set; }
        public Admin Admin { get; set; } // Many-to-One with Admin
    }
}
