using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class Reservation : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string RoomNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string PictureUrl { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // Foreign keys
        public int ApartmentId { get; set; }
        public int BedId { get; set; }
        public int StudentId { get; set; }

        // Navigation properties
        public Apartment Apartment { get; set; }
        public Beds Bed { get; set; }
        public Students Student { get; set; }

    }
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}
