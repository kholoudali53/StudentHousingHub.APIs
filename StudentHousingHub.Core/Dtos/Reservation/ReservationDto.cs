using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Dtos.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string RoomNumber { get; set; }
        public string NationalId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        public string PictureUrl { get; set; }
        public string Status { get; set; } = "Pending";

        public int ApartmentId { get; set; }
        public int BedId { get; set; }
        public int StudentId { get; set; }

    }
}
