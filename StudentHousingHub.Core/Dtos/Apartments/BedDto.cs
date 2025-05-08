using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Apartments
{
    public class BedDto
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public bool IsAvailable { get; set; } = true;

        // FK
        public int RoomId { get; set; }
    }
}
