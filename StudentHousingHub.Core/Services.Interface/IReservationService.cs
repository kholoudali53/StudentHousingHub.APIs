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
        Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto);
    }
}



//Task ConfirmReservation(int reservationId);

//Task CancelReservation(int reservationId);