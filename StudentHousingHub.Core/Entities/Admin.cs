using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Admin : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalID { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }


        // Relationships
        public ICollection<Owners> Owners { get; set; } // One-to-Many with Owner
        public ICollection<Students> Students { get; set; } // One-to-Many with Student
        public ICollection<Reports> Reports { get; set; } // One-to-Many with Report
    }
}
