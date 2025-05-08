using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Rooms : BaseEntity<int>
    {
        public string RoomNumber { get; set; }
        public int TotalBeds { get; set; }
        public int AvailableBeds { get; set; }
        public decimal PricePerBed { get; set; }
        public bool IsAvailable => AvailableBeds > 0;

        // Foreign Key 
        public int ApartmentId { get; set; }

        // Navigation Property
        public Apartment Apartment { get; set; }

        public List<Beds> Beds { get; set; } = new List<Beds>();

        //public ICollection<Beds> bed { get; set; } = new List<Beds>();
    }
}
