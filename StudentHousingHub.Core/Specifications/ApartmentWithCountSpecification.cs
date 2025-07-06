using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Specifications
{
    public class ApartmentWithCountSpecification : BaseSpecification<Entities.Apartment, int>
    {
        public ApartmentWithCountSpecification(ApartmentSpecParameters apartmentSpecParameters)
            : base(BuildCriteria(apartmentSpecParameters))
        {
        }

        private static Expression<Func<Entities.Apartment, bool>> BuildCriteria(ApartmentSpecParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.SearchType == 2) // Advanced Search
            {
                return p =>
                    //(string.IsNullOrEmpty(parameters.UniversitySearch) ||
                    //(p.UniversityName != null && p.UniversityName.ToLower().Contains(parameters.UniversitySearch.ToLower()))) &&
                    (string.IsNullOrEmpty(parameters.Address) ||
                    (p.Address != null && p.Address.ToLower().Contains(parameters.Address.ToLower()))) &&
                    (string.IsNullOrEmpty(parameters.Gender) ||
                    (p.Gender != null && p.Gender.ToLower() == parameters.Gender.ToLower())) &&
                    (string.IsNullOrEmpty(parameters.Floor) ||
                    (p.Floor != null && p.Floor.ToLower() == parameters.Floor.ToLower())) &&
                    (!parameters.PriceFrom.HasValue || p.PriceMonthly >= parameters.PriceFrom) &&
                    (!parameters.PriceTo.HasValue || p.PriceMonthly <= parameters.PriceTo) &&
                    (!parameters.OwnerId.HasValue || parameters.OwnerId == p.OwnerId);
            }
            else // Basic Search
            {
                return p =>
                    (string.IsNullOrEmpty(parameters.SearchWithUniversityName) ||
                    (p.UniversityName != null && p.UniversityName.ToLower().Contains(parameters.SearchWithUniversityName.ToLower()))) &&
                    (!parameters.OwnerId.HasValue || parameters.OwnerId == p.OwnerId);
            }
        }
    }
}