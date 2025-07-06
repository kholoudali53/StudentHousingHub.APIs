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
        [ProducesResponseType(typeof(ReservationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize]
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

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize]
        public async Task<IActionResult> SearchReservations([FromQuery] SearchReservationDto searchDto)
        {
            try
            {
                _logger.LogInformation("Searching reservations with criteria");
                var results = await _reservationService.SearchReservationsAsync(searchDto);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching reservations");
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpPut("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CancelReservation(int id)
        {
            _logger.LogInformation("Cancellation request for reservation {ReservationId}", id);

            try
            {
                var result = await _reservationService.CancelReservationAsync(id);

                if (result)
                {
                    return Ok(new { Message = "Reservation cancelled successfully" });
                }

                return BadRequest(new ApiErrorResponse(400, "Reservation could not be cancelled"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling reservation {ReservationId}", id);
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

    }
}
