using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Reservation
{
    public class SearchReservationDto
    {
        public int? ApartmentId { get; set; }
        public int? StudentId { get; set; }
        public string? NationalId { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
