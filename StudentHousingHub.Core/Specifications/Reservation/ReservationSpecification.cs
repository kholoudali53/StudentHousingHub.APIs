using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Specifications.Reservation
{
    public class ReservationSpecification : BaseSpecification<Entities.Reservation, int>
    {
        public ReservationSpecification(SearchReservationDto searchDto)
        : base(r =>
            (!searchDto.ApartmentId.HasValue || r.ApartmentId == searchDto.ApartmentId) &&
            (!searchDto.StudentId.HasValue || r.StudentId == searchDto.StudentId) &&
            (string.IsNullOrEmpty(searchDto.NationalId) || r.NationalId.Contains(searchDto.NationalId)) &&
            (string.IsNullOrEmpty(searchDto.Status) || r.Status.ToString() == searchDto.Status) && 
            (!searchDto.FromDate.HasValue || r.CheckInDate >= searchDto.FromDate) &&
            (!searchDto.ToDate.HasValue || r.CheckOutDate <= searchDto.ToDate))
        {
            Includes.Add(r => r.Apartment);
            Includes.Add(r => r.Student);
            Includes.Add(r => r.Bed);
        }
    }
}
