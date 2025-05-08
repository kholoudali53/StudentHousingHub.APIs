using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHousingHub.APIs.Errors;
using StudentHousingHub.Core;
using StudentHousingHub.Core.Dtos.Reservation;
using StudentHousingHub.Core.Dtos.Rooms;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Helper;
using StudentHousingHub.Core.Services.Interface;
using StudentHousingHub.Core.Specifications;

namespace StudentHousingHub.APIs.Controllers
{
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(IReservationService reservationService, ILogger<ReservationsController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            _logger.LogInformation("Received reservation creation request");

            try
            {
                if (reservationDto == null)
                {
                    _logger.LogWarning("Request body is null");
                    return BadRequest(new ApiErrorResponse(400, "Reservation data is required"));
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new ApiErrorResponse(400, string.Join(", ", errors)));
                }
                _logger.LogDebug("Calling reservation service");
                var result = await _reservationService.CreateReservationAsync(reservationDto);

                _logger.LogInformation("Reservation created successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create reservation. Message: {Message}", ex.Message);
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }
    }
}

//if (reservationDto == null)
//{
//    return BadRequest(new ApiErrorResponse(400, "Reservation information must be sent"));
//}

//// التحقق من صحة البيانات
//if (!ModelState.IsValid)
//{
//    var errors = ModelState.Values
//        .SelectMany(v => v.Errors)
//        .Select(e => e.ErrorMessage)
//        .ToList();

//    return BadRequest(new ApiErrorResponse(400, "invalid data"));
//}

//try
//{
//    /*if (!ModelState.IsValid)
//        return BadRequest(ModelState);*/

//    var result = await _reservationService.CreateReservationAsync(reservationDto);
//    return Ok(result);

//}
//catch (Exception ex)
//{
//    return BadRequest(new ApiErrorResponse(400, "Error While creating reservation"));
//}
