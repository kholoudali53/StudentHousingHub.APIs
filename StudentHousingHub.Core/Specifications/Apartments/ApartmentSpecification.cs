using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Specifications.Apartments
{
    public class ApartmentSpecification : BaseSpecification<Entities.Apartment, int>
    {
        public ApartmentSpecification(int id) : base(P => P.id == id)
        {
            ApplyIncludes();
        }

        public ApartmentSpecification(ApartmentSpecParameters apartmentSpecParameters) : base(
            BuildCriteria(apartmentSpecParameters))
        {
            ApplySorting(apartmentSpecParameters);
            ApplyIncludes();
            ApplyPagination(apartmentSpecParameters);
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

        private void ApplySorting(ApartmentSpecParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                switch (parameters.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.TotalPrice);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.TotalPrice);
                        break;
                    default:
                        AddOrderBy(P => P.UniversityName);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.UniversityName);
            }
        }

        private void ApplyPagination(ApartmentSpecParameters parameters)
        {
            ApplyPagination(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }

        private void ApplyIncludes()
        {
            Includes.Add(a => a.Owner);
            Includes.Add(a => a.Rooms);
            Includes.Add(a => a.Beds);
        }
    }
}
/*
       public RoomSpecification(RoomSpecParameters roomSpecParameters)
          : base
          (
               P =>

                  (string.IsNullOrEmpty(roomSpecParameters.UniversitySearch) ||
                  P.UniversityName.ToLower().Contains(roomSpecParameters.UniversitySearch))

               &&

                   (string.IsNullOrEmpty(roomSpecParameters.SearchWithIcon) ||
                   P.Title.ToLower().Contains(roomSpecParameters.SearchWithIcon) ||
                   P.Description.ToLower().Contains(roomSpecParameters.SearchWithIcon))
                   &&
                   (string.IsNullOrEmpty(roomSpecParameters.Location) ||
                   P.Address.ToLower().Contains(roomSpecParameters.Location))
                   &&
                   (string.IsNullOrEmpty(roomSpecParameters.Gender) ||
                   P.Gender.ToLower() == roomSpecParameters.Gender)
                   &&
                   (!roomSpecParameters.Floor.HasValue ||
                   P.Floor == roomSpecParameters.Floor)
                   &&
                   (!roomSpecParameters.PriceFrom.HasValue ||
                   P.Price >= roomSpecParameters.PriceFrom)
                   &&
                   (!roomSpecParameters.PriceTo.HasValue ||
                   P.Price <= roomSpecParameters.PriceTo)
                   &&
                  (roomSpecParameters.OwnerId == null ||
                  P.OwnerId == roomSpecParameters.OwnerId)

               &&

                 (P.OwnerId == roomSpecParameters.OwnerId)


          )
       {

           // name, priceAsc, priceDesc

           if (!string.IsNullOrEmpty(roomSpecParameters.Sort))
           {
               switch (roomSpecParameters.Sort)
               {
                   case "PriceAsc":
                       AddOrderBy(P => P.Price);
                       break;
                   case "PriceDesc":
                       AddOrderByDescending(P => P.Price);
                       break;
                   default:
                       AddOrderBy(P => P.id);
                       break;
               }
           }
           else
           {
               AddOrderBy(P => P.id);
           }


           ApplyIncludes();

           // 900
           // PageSize = 50
           // PageIndex = 3
           ApplyPagination(roomSpecParameters.PageSize * (roomSpecParameters.PageIndex - 1), roomSpecParameters.PageSize);
       }*/

/*
 public class RoomSpecification : BaseSpecification<Entities.Rooms, int>
    {
        public RoomSpecification(int id) : base(P => P.id == id)
        {
            ApplyIncludes();
        }

        public RoomSpecification(RoomSpecParameters roomSpecParameters) : base(
            P => roomSpecParameters.IsAdvancedSearch
                ? AdvancedSearchPredicate(P, roomSpecParameters)
                : BasicSearchPredicate(P, roomSpecParameters))
        {
            ApplySorting(roomSpecParameters);
            ApplyIncludes();
            ApplyPagination(roomSpecParameters);
        }

        private Expression<Func<Entities.Rooms, bool>> BasicSearchPredicate(
            Entities.Rooms room,
            RoomSpecParameters parameters)
        {
            return P =>
                (string.IsNullOrEmpty(parameters.UniversitySearch) ||
                P.UniversityName.ToLower().Contains(parameters.UniversitySearch))
                &&
                (!parameters.OwnerId.HasValue || parameters.OwnerId == P.OwnerId);
        }

        private Expression<Func<Entities.Rooms, bool>> AdvancedSearchPredicate(
            Entities.Rooms room,
            RoomSpecParameters parameters)
        {
            return P =>
                (string.IsNullOrEmpty(parameters.UniversitySearch) ||
                P.UniversityName.ToLower().Contains(parameters.UniversitySearch))
                &&
                (string.IsNullOrEmpty(parameters.Location) ||
                P.Address.ToLower().Contains(parameters.Location))
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

        private void ApplySorting(RoomSpecParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                switch (parameters.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.UniversityName);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.UniversityName);
            }
        }

        private void ApplyPagination(RoomSpecParameters parameters)
        {
            ApplyPagination(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Owner);
        }
    }
 */


/*
 
public class RoomSpecification : BaseSpecification<Entities.Rooms, int>
    {
        public RoomSpecification(int id) : base(P => P.id == id)
        {
            ApplyIncludes();
        }

        public RoomSpecification(RoomSpecParameters roomSpecParameters) : base(
            P =>
             (string.IsNullOrEmpty(roomSpecParameters.UniversitySearch) ||
                   P.UniversityName.ToLower().Contains(roomSpecParameters.UniversitySearch))
            &&
            (!roomSpecParameters.OwnerId.HasValue || roomSpecParameters.OwnerId == P.OwnerId)
            )
        {
            // name, priceAsc, priceDesc

            if (!string.IsNullOrEmpty(roomSpecParameters.Sort))
            {
                switch (roomSpecParameters.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.UniversityName);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.UniversityName);
            }

            ApplyIncludes();

            // 900
            // Page Size = 50
            // Page Index = 3
            ApplyPagination(roomSpecParameters.PageSize * (roomSpecParameters.PageIndex - 1), roomSpecParameters.PageSize);
            
        }
       
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Owner);
        }
    }
 
 */