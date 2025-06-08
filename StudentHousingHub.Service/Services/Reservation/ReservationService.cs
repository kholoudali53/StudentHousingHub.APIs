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
    }

}

/*
 * private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ReservationDto> MakeReservation(ReservationDto reservationDto)
        {
            var apartmentRepo = _unitOfWork.Repository<Apartment, int>();
            var reservationRepo = _unitOfWork.Repository<Core.Entities.Reservation, int>();
            var bedRepo = _unitOfWork.Repository<Beds, int>();
            var roomRepo = _unitOfWork.Repository<Core.Entities.Rooms, int>();

            // Get apartment with included rooms and beds
            var apartment = await apartmentRepo.GetByIdAsync(
                reservationDto.ApartmentId,
                include: query => query.Include(a => a.Rooms)
                                      .ThenInclude(r => r.Beds));

            if (apartment == null)
                throw new NotFoundException("Apartment not found");

            if (!apartment.IsAvailable)
                throw new BadRequestException("Apartment is not available");

            var bed = apartment.Rooms
                .SelectMany(r => r.Beds)
                .FirstOrDefault(b => b.id == reservationDto.BedId && b.IsAvailable);

            if (bed == null)
                throw new BadRequestException("Bed is not available");

            var reservation = _mapper.Map<Core.Entities.Reservation>(reservationDto);
            reservation.Status = ReservationStatus.Pending;

            await reservationRepo.AddAsync(reservation);

            // Update bed status
            bed.IsAvailable = false;
            bedRepo.Update(bed);

            // Update room available beds
            var room = bed.Room;
            room.AvailableBeds--;
            roomRepo.Update(room);

            // Update apartment available beds
            apartment.AvailableBeds--;
            apartmentRepo.Update(apartment);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ReservationDto>(reservation);


        }

        public async Task ConfirmReservation(int reservationId)
        {
            var reservationRepo = _unitOfWork.Repository<Core.Entities.Reservation, int>();

            var reservation = await reservationRepo.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            reservation.Status = ReservationStatus.Confirmed;
            reservationRepo.Update(reservation);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CancelReservation(int reservationId)
        {
            var reservationRepo = _unitOfWork.Repository<Core.Entities.Reservation, int>();
            var bedRepo = _unitOfWork.Repository<Beds, int>();
            var roomRepo = _unitOfWork.Repository<Core.Entities.Rooms, int>();
            var apartmentRepo = _unitOfWork.Repository<Apartment, int>();

            var reservation = await reservationRepo.GetByIdAsync(reservationId,
                include: r => r.Include(x => x.Bed)
                              .ThenInclude(b => b.Room)
                              .Include(x => x.Apartment));

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            reservation.Status = ReservationStatus.Cancelled;
             reservationRepo.Update(reservation);

            // Update bed status
            var bed = reservation.Bed;
            bed.IsAvailable = true;
             bedRepo.Update(bed);

            // Update room available beds
            var room = bed.Room;
            room.AvailableBeds++;
             roomRepo.Update(room);

            // Update apartment available beds
            var apartment = reservation.Apartment;
            apartment.AvailableBeds++;
             apartmentRepo.Update(apartment);

            await _unitOfWork.CompleteAsync();
        }
 */