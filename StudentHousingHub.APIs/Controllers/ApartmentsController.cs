using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentHousingHub.APIs.Attributes;
using StudentHousingHub.APIs.Errors;
using StudentHousingHub.Core.Dtos.Apartments;
using StudentHousingHub.Core.Dtos.Rooms;
using StudentHousingHub.Core.Helper;
using StudentHousingHub.Core.Services.Interface;
using StudentHousingHub.Core.Specifications;
using StudentHousingHub.Service.Services.Rooms;

namespace StudentHousingHub.APIs.Controllers
{
    public class ApartmentsController : BaseApiController
    {
        private readonly IApartmentService _apartmentsService;

        public ApartmentsController(IApartmentService apartmentsService)
        {
            _apartmentsService = apartmentsService;
        }

        [HttpGet] // Get BaseURL/api/Apartments
        [ProducesResponseType(typeof(PaginationResponse<ApartmentDto>), StatusCodes.Status200OK)]
       // [Cached(10)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ApartmentDto>>> GetAllApartments([FromQuery] ApartmentSpecParameters apartmentSpecParameters)
        {

            var result = await _apartmentsService.GetAllApartmentsAsync(apartmentSpecParameters);
            return Ok(result);
        }
        
        [ProducesResponseType(typeof(ApartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetApartmentById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));

            var result = await _apartmentsService.GetApartmentByIdAsync(id.Value);
            if (result is null) return NotFound(new ApiErrorResponse(400, $"The Apartment With id: {id} Not Found"));

            return Ok(result);
        }


        [HttpPost("AddApartment")]
        [ProducesResponseType(typeof(ApartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult> AddApartment([FromBody] AddApartmentDto apartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResponse(400, "Invalid input data"));
                }

                var result = await _apartmentsService.AddApartmentAsync(apartmentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, "An error occurred while adding the apartment"));
            }
        }
    }
}