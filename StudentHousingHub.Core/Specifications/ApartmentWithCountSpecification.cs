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
                    (!parameters.Floor.HasValue || p.Floor == parameters.Floor) &&
                    (!parameters.PriceFrom.HasValue || p.TotalPrice >= parameters.PriceFrom) &&
                    (!parameters.PriceTo.HasValue || p.TotalPrice <= parameters.PriceTo) &&
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
/*
 public class RoomWithCountSpecification : BaseSpecification<Entities.Rooms, int>
    {
        public RoomWithCountSpecification(RoomSpecParameters roomSpecParameters)
            : base(P => roomSpecParameters.IsAdvancedSearch
                ? AdvancedSearchPredicate(P, roomSpecParameters)
                : BasicSearchPredicate(P, roomSpecParameters))
        {
        }

        private static Expression<Func<Entities.Rooms, bool>> BasicSearchPredicate(
            Entities.Rooms room,
            RoomSpecParameters parameters)
        {
            return P =>
                (string.IsNullOrEmpty(parameters.UniversitySearch) ||
                P.UniversityName.ToLower().Contains(parameters.UniversitySearch))
                &&
                (!parameters.OwnerId.HasValue || parameters.OwnerId == P.OwnerId);
        }

        private static Expression<Func<Entities.Rooms, bool>> AdvancedSearchPredicate(
            Entities.Rooms room,
            RoomSpecParameters parameters)
        {
            return P =>
                (string.IsNullOrEmpty(parameters.UniversitySearch) ||
                P.UniversityName.ToLower().Contains(parameters.UniversitySearch))
                &&
                (string.IsNullOrEmpty(parameters.Location) ||
                (P.Address != null && P.Address.ToLower().Contains(parameters.Location)))
                &&
                (string.IsNullOrEmpty(parameters.Gender) ||
                P.Gender.ToLower() == parameters.Gender)
                &&
                (!parameters.Floor.HasValue || P.Floor == parameters.Floor)
                &&
                (!parameters.PriceFrom.HasValue || P.Price >= parameters.PriceFrom)
                &&
                (!parameters.PriceTo.HasValue || P.Price <= parameters.PriceTo)
                &&
                (!parameters.OwnerId.HasValue || parameters.OwnerId == P.OwnerId);
        }
    }
 */

/*
 
 public class RoomWithCountSpecification : BaseSpecification<Entities.Rooms, int>
    {
        public RoomWithCountSpecification(RoomSpecParameters roomSpecParameters)
            : base
            (
                 P =>
                 (string.IsNullOrEmpty(roomSpecParameters.UniversitySearch) ||
                    P.UniversityName.ToLower().Contains(roomSpecParameters.UniversitySearch))
                 &&
                 (!roomSpecParameters.OwnerId.HasValue || roomSpecParameters.OwnerId == P.OwnerId)
                 
                 

            )
        {
        }
    }
 
 */