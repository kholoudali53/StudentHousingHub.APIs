using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Students : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NationalID { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        // Foreign Key for Admin
        public int AdminId { get; set; }
        public Admin Admin { get; set; }


        // Relationships

        public ICollection<Reports> Reports { get; set; } = new List<Reports>();

        public ICollection<ContactUs> ReceivedMessages { get; set; } = new List<ContactUs>();


    }
}
