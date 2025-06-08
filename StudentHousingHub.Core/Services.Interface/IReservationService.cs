using StudentHousingHub.Core.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Services.Interface
{
    public interface IReservationService
    {
        Task<ReservationResponseDto> CreateReservationAsync(ReservationDto reservationDto);
        Task<IEnumerable<ReservationDto>> SearchReservationsAsync(SearchReservationDto searchDto);
    }
}



//Task ConfirmReservation(int reservationId);

//Task CancelReservation(int reservationId);