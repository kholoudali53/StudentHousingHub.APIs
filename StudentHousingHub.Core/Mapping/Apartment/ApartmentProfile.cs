using AutoMapper;
using Microsoft.Extensions.Configuration;
using StudentHousingHub.Core.Dtos.Apartments;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Dtos.Rooms;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentHousingHub.Core.Mapping.Apartment
{
    public class ApartmentProfile : Profile
    {
        private const int MaxImagesToShow = 5;
        private readonly IConfiguration _configuration;

        public ApartmentProfile(IConfiguration configuration)
        {
            _configuration = configuration;

            CreateMap<Entities.Apartment, ApartmentDto>()
            .ForMember(d => d.OwnerName, O => O.MapFrom(s => $"{s.Owner.FirstName} {s.Owner.LastName}".Trim()))
            .ForMember(dest => dest.AvailableRooms, opt => opt.MapFrom(src => src.Rooms))
            .ForMember(d => d.Images, O => O.MapFrom(new PictureUrlResolver(configuration)))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PriceMonthly));

            CreateMap<AddApartmentDto, Entities.Apartment>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.AvailableRooms));

            CreateMap<Beds, BedDto>().ReverseMap();

            CreateMap<Rooms, RoomDto>()
            .ForMember(dest => dest.AvailableBeds, opt => opt.MapFrom(src => src.Beds.Count(b => b.IsAvailable)))
            .ForMember(dest => dest.Beds, opt => opt.MapFrom(src => src.Beds)).ReverseMap();

            CreateMap<Owners, OwnerDto>();
        }
    }
}