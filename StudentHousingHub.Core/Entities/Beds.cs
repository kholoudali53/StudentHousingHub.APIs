using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Beds : BaseEntity<int>
    {
        public string BedNumber { get; set; }
        public bool IsAvailable { get; set; } = true;

        // 
        public int RoomId { get; set; }
        public Rooms Room { get; set; }
    }
}
