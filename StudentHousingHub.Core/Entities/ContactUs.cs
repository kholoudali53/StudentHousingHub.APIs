using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class ContactUs : BaseEntity<int>
    {
        public string CommunicationType { get; set; } // "WhatsApp", "Phone", or "Email"
        public string? ChatOnWhatsapp { get; set; }
        public string? EmailUs { get; set; }
        public string? CallUs { get; set; }

        // Foreign Key for Owner
        public int OwnerId { get; set; }
        public Owners Owner { get; set; }

        // Foreign Key for Student
        public int StudentId { get; set; }
        public Students Student { get; set; }

    }
}
