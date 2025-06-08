using AutoMapper;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Mapping.Reservation
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            //CreateMap<Entities.Reservation, ReservationDto>().ReverseMap();
            CreateMap<ReservationDto, Entities.Reservation>()
            .ForMember(dest => dest.Status,
                      opt => opt.MapFrom(src => Enum.Parse<ReservationStatus>(src.Status)));

            CreateMap<Entities.Reservation, ReservationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}