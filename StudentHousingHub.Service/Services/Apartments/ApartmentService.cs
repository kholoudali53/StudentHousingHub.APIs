using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentHousingHub.Core;
using StudentHousingHub.Core.Dtos.Apartments;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Dtos.Rooms;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Helper;
using StudentHousingHub.Core.Services.Interface;
using StudentHousingHub.Core.Specifications;
using StudentHousingHub.Core.Specifications.Apartments;
using StudentHousingHub.Repository;
using StudentHousingHub.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Service.Services.Rooms
{
    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginationResponse<ApartmentDto>> GetAllApartmentsAsync(ApartmentSpecParameters apartmentSpecParameters)
        {
            var spec = new ApartmentSpecification(apartmentSpecParameters);

            var apartments = await _unitOfWork.Repository<Apartment, int>().GetAllWithSpecAsync(spec);

            foreach (var apartment in apartments)
            {
                await _unitOfWork.Repository<Apartment, int>()
                    .GetByIdAsync(apartment.id, include: q => q
                        .Include(a => a.Owner)
                        .Include(a => a.Rooms)
                            .ThenInclude(r => r.Beds));
            }

            var mappedRooms = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

            var countSpec = new ApartmentWithCountSpecification(apartmentSpecParameters);
            var count = await _unitOfWork.Repository<Apartment, int>().GetCountAsync(countSpec);
            return new PaginationResponse<ApartmentDto>(apartmentSpecParameters.PageSize, apartmentSpecParameters.PageIndex, 0, mappedRooms);
        
        }

        public async Task<ApartmentDto> GetApartmentByIdAsync(int id)
        {
            var spec = new ApartmentSpecification(id);
            var apartment = await _unitOfWork.Repository<Apartment, int>()
                .GetWithSpecAsync(spec);

            if (apartment == null)
                return null;

            if (apartment.Rooms != null && apartment.Rooms.Any(r => r.Beds == null || !r.Beds.Any()))
            {
                // Load all beds for all rooms in one query
                var roomIds = apartment.Rooms.Select(r => r.id).ToList();
                var allBeds = await _context.Beds
                    .Where(b => roomIds.Contains(b.RoomId))
                    .ToListAsync();

                // Assign beds to their rooms
                foreach (var room in apartment.Rooms)
                {
                    room.Beds = allBeds.Where(b => b.RoomId == room.id).ToList();
                }
            }

            return _mapper.Map<ApartmentDto>(apartment);
        }


        public async Task<ApartmentDto> AddApartmentAsync(AddApartmentDto apartmentDto)
        {
            var owner = await _unitOfWork.Repository<Owners, int>().GetByIdAsync(apartmentDto.OwnerId);
            if (owner == null)
            {
                throw new KeyNotFoundException($"Owner with ID {apartmentDto.OwnerId} not found");
            }

            var apartment = _mapper.Map<Apartment>(apartmentDto);

            await _unitOfWork.Repository<Apartment, int>().AddAsync(apartment);
            await _unitOfWork.CompleteAsync();

            var fullApartment = await _unitOfWork.Repository<Apartment, int>()
           .GetByIdAsync(apartment.id, include: q => q
           .Include(a => a.Owner)
           .Include(a => a.Rooms)
           .ThenInclude(r => r.Beds));

            return _mapper.Map<ApartmentDto>(fullApartment);
        }
    }
}
