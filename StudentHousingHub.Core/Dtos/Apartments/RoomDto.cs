using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Apartments
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int TotalBeds { get; set; }
        public int AvailableBeds { get; set; }
        public decimal PricePerBed { get; set; }
        public bool IsAvailable => AvailableBeds > 0;

        // Foreign Key 
        public int ApartmentId { get; set; }

        public List<BedDto> Beds { get; set; } = new List<BedDto>();
    }
}
