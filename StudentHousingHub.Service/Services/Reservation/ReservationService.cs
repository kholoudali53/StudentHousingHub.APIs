using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudentHousingHub.Core;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Exceptions;
using StudentHousingHub.Core.Services.Interface;
using StudentHousingHub.Core.Specifications.Reservation;
using StudentHousingHub.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Service.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReservationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ReservationResponseDto> CreateReservationAsync(ReservationDto reservationDto)
        {
            try
            {
                _logger.LogDebug("Starting reservation creation process");

                if (reservationDto == null)
                {
                    _logger.LogError("Reservation data is null");
                    throw new ArgumentNullException(nameof(reservationDto));
                }


                // verify apartment exists
                _logger.LogDebug("verifying apartment with id {apartmentid}", reservationDto.ApartmentId);
                var apartment = await _unitOfWork.Repository<Apartment, int>().GetByIdAsync(reservationDto.ApartmentId);

                if (apartment == null)
                {
                    _logger.LogError("apartment with id {apartmentid} not found", reservationDto.ApartmentId);
                    throw new KeyNotFoundException($"apartment with id {reservationDto.ApartmentId} not found");
                }


                // Verify bed exists and is available
                _logger.LogDebug("Verifying bed with ID {BedId}", reservationDto.BedId);
                var bed = await _unitOfWork.Repository<Beds, int>().GetByIdAsync(reservationDto.BedId);

                if (bed == null)
                {
                    _logger.LogError("Bed with ID {BedId} not found", reservationDto.BedId);
                    throw new KeyNotFoundException($"Bed with ID {reservationDto.BedId} not found");
                }


                if (!bed.IsAvailable)
                {
                    _logger.LogWarning("Bed {BedId} is not available", reservationDto.BedId);
                    throw new InvalidOperationException($"Bed {reservationDto.BedId} is not available");
                }



                if (!Enum.TryParse<ReservationStatus>(reservationDto.Status, true, out var status))
                {
                    throw new ArgumentException("Invalid reservation status");
                }


                // Create reservation
                var reservation = _mapper.Map<Core.Entities.Reservation>(reservationDto);

                // Add reservation
                await _unitOfWork.Repository<Core.Entities.Reservation, int>().AddAsync(reservation);

                // Update bed availability
                bed.IsAvailable = false;
                _unitOfWork.Repository<Beds, int>().Update(bed);

                // Save changes
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Reservation created successfully for bed {BedId}", reservation.BedId);

                return new ReservationResponseDto { ReservationId = reservation.BedId };
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null
                ? $"Inner Exception: {ex.InnerException.Message}"
                : "No inner exception.";

                _logger.LogError(ex, "Error occurred while creating reservation. {InnerMessage}", innerMessage);

                throw;
            }
        }

        public async Task<IEnumerable<ReservationDto>> SearchReservationsAsync(SearchReservationDto searchDto)
        {
            var spec = new ReservationSpecification(searchDto);
            var reservations = await _unitOfWork.Repository<Core.Entities.Reservation, int>()
                .GetAllWithSpecAsync(spec);

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            try
            {
                _logger.LogDebug("Starting reservation cancellation process for reservation ID {ReservationId}", reservationId);

                // Get the reservation
                var reservation = await _unitOfWork.Repository<Core.Entities.Reservation, int>().GetByIdAsync(reservationId);

                if (reservation == null)
                {
                    _logger.LogError("Reservation with ID {ReservationId} not found", reservationId);
                    throw new KeyNotFoundException($"Reservation with ID {reservationId} not found");
                }

                // Check if reservation is already cancelled
                if (reservation.Status == ReservationStatus.Cancelled)
                {
                    _logger.LogWarning("Reservation {ReservationId} is already cancelled", reservationId);
                    return false;
                }

                if (reservation.Status == ReservationStatus.Completed)
                {
                    _logger.LogWarning("Cannot cancel completed reservation {ReservationId}", reservationId);
                    throw new InvalidOperationException("Cannot cancel a completed reservation");
                }

                // Get the associated bed
                var bed = await _unitOfWork.Repository<Beds, int>().GetByIdAsync(reservation.BedId);
                if (bed == null)
                {
                    _logger.LogError("Associated bed with ID {BedId} not found", reservation.BedId);
                    throw new KeyNotFoundException($"Associated bed with ID {reservation.BedId} not found");
                }

                // Update reservation status
                reservation.Status = ReservationStatus.Cancelled;
                _unitOfWork.Repository<Core.Entities.Reservation, int>().Update(reservation);

                // Mark bed as available again
                bed.IsAvailable = true;
                _unitOfWork.Repository<Beds, int>().Update(bed);

                // Save changes
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Reservation {ReservationId} cancelled successfully", reservationId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling reservation {ReservationId}", reservationId);
                throw;
            }
        }

    }

}
