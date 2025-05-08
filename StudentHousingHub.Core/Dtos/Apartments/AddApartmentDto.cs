using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Apartments
{
    public class AddApartmentDto
    {
        public List<string> Images { get; set; } = new List<string>();
        public string UniversityName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public decimal Space { get; set; }
        public int Floor { get; set; }
        public List<RoomDto> AvailableRooms { get; set; } = new List<RoomDto>();
        public Amenities? Amenities { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }


        // Foreign Key for Owner
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
