using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Specifications
{
    public class ApartmentSpecParameters
    {
        public string? Sort { get; set; }
        public int? OwnerId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public int SearchType { get; set; } = 1;


        // GET /api/rooms?SearchType=1&UniversitySearch=القاهرة
        private string? searchWithUniversityName;

        public string? SearchWithUniversityName
        {
            get { return searchWithUniversityName; }
            set { searchWithUniversityName = value?.ToLower(); }
        }

        // GET /api/rooms?SearchType=2&Address=المعادي&Gender=ذكر&Floor=2&PriceFrom=500&PriceTo=1000
        private string? address;
        private string? gender;
        private int? floor;

        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        //public string? Address
        //{
        //    get => Address;
        //    set => Address = value?.ToLower();
        //}
        public string? Address { get; set; }
        [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        //public string? Gender
        //{
        //    get => gender;
        //    //set => gender = value?.ToLower();      
        //}
        public string? Gender { get; set; }

        [Range(1, 50, ErrorMessage = "Floor must be between 1 and 50")]
        public int? Floor
        {
            get => floor;
            set => floor = value;
        }

        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100,000")]
        public decimal? PriceFrom { get; set; }

        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100,000")]
        public decimal? PriceTo { get; set; }

    }
}
//[FromQuery] string? sort, [FromQuery] int? OwnerId, [FromQuery] int? PageSize = 5, [FromQuery] int? PageIndex = 1
