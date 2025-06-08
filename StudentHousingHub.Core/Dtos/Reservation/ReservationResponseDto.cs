using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Reservation
{
    public class ReservationResponseDto
    {
        public int ReservationId { get; set; }
        public string Message { get; set; } = "Reservation created successfully";
    }
}
