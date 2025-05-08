using AutoMapper;
using Microsoft.Extensions.Configuration;
using StudentHousingHub.Core.Dtos.Apartments;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Mapping.Apartment
{
    public class PictureUrlResolver : IValueResolver<Entities.Apartment, ApartmentDto, List<string>>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> Resolve(Entities.Apartment source, ApartmentDto destination, List<string> destMember, ResolutionContext context)
        {
            var result = new List<string>();
            if (source.Images == null || source.Images.Count == 0)
                return result;

            // أخذ أقصى 5 صور وإضافة الرابط الأساسي لكل صورة
            var imagesToTake = Math.Min(source.Images.Count, 5);
            for (int i = 0; i < imagesToTake; i++)
            {
                if (!string.IsNullOrEmpty(source.Images[i]))
                {
                    result.Add($"{_configuration["BASEURL"]}/{source.Images[i]}");
                }
            }

            return result;
           // return string.Empty;
        }
    }
}