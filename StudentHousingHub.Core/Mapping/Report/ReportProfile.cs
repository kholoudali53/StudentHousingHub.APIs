using AutoMapper;
using StudentHousingHub.Core.Dtos.Reports;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Mapping.Report
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Reports, ReportDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
            .ReverseMap();

            CreateMap<CreateReportDto, ReportDto>().ReverseMap();
        }
    }
}
