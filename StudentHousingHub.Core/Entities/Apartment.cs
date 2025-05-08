using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHousingHub.Core.Dtos.Apartments;

namespace StudentHousingHub.Core.Entities
{
    public enum Amenities
    {
        None = 0,
        Wifi = 1,
        SharedKitchen = 2,
        AirConditioning = 4,
        TV = 8,
        WashingMachine = 16
    }
    public class Apartment : BaseEntity<int>
    {
        public List<string> Images { get; set; } = new List<string>();


        public string UniversityName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public decimal Space { get; set; }
        public int Floor { get; set; }
        public List<Rooms> Rooms { get; set; } = new List<Rooms>();
        public List<Beds> Beds { get; set; } = new List<Beds>();
        public Amenities? Amenities { get; set; }
        public string Description { get; set; }

        public decimal TotalPrice { get; set; }

        // Foreign Key for Owner
        public int OwnerId { get; set; }
        public Owners Owner { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        //public ICollection<Rooms> Room { get; set; } = new List<Rooms>();

    }
}
// لما اعمل ادد البيانات تتسجل في الداتا بيز
// 