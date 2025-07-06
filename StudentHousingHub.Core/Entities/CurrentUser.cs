using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public Students student { get; set; }
        public int OwnerId { get; set; }
        public Owners owner { get; set; }
        public bool IsAdmin { get; set; }
        public Admin admin { get; set; }
    }
}
