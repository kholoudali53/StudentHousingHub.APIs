using StudentHousingHub.Core.Dtos.Apartments;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Dtos.Rooms;
using StudentHousingHub.Core.Helper;
using StudentHousingHub.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Services.Interface
{
    public interface IApartmentService
    {
        Task<PaginationResponse<ApartmentDto>> GetAllApartmentsAsync(ApartmentSpecParameters roomSpecParameters);
        Task<ApartmentDto> GetApartmentByIdAsync(int id);

        Task<ApartmentDto> AddApartmentAsync(AddApartmentDto apartmentDto);
    }
}
