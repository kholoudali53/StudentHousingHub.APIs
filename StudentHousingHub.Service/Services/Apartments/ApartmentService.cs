using AutoMapper;
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

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ApartmentDto>> GetAllApartmentsAsync(ApartmentSpecParameters apartmentSpecParameters)
        {
            var spec = new ApartmentSpecification(apartmentSpecParameters);

            var apartments = await _unitOfWork.Repository<Core.Entities.Apartment, int>().GetAllWithSpecAsync(spec);
            var mappedRooms = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

            var countSpec = new ApartmentWithCountSpecification(apartmentSpecParameters);
            var count = await _unitOfWork.Repository<Core.Entities.Apartment, int>().GetCountAsync(countSpec);
            return new PaginationResponse<ApartmentDto>(apartmentSpecParameters.PageSize, apartmentSpecParameters.PageIndex, 0, mappedRooms);
        }

        public async Task<ApartmentDto> GetApartmentByIdAsync(int id)
        {
            var spec = new ApartmentSpecification(id);
            return _mapper.Map<ApartmentDto>(await _unitOfWork.Repository<Core.Entities.Apartment, int>().GetWithSpecAsync(spec));
        }


        public async Task<ApartmentDto> AddApartmentAsync(AddApartmentDto apartmentDto)
        {
            /*
            var ownerExists = await _unitOfWork.Repository<Owners, int>().GetByIdAsync(apartmentDto.OwnerId);
            if (ownerExists == null)
            {
                throw new Exception("Owner not found");
            }*/

            var apartment = _mapper.Map<Apartment>(apartmentDto);

            await _unitOfWork.Repository<Apartment, int>().AddAsync(apartment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ApartmentDto>(apartment);
        }
    }
}
