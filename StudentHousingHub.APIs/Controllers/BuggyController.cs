using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHousingHub.APIs.Errors;
using StudentHousingHub.Repository.Data.Contexts;

namespace StudentHousingHub.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly AppDbContext _context;

        public BuggyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // Get: /api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var rooms = await _context.Rooms.FindAsync(100);
            if (rooms is null) return NotFound(new ApiErrorResponse(404));
            return Ok(rooms);
        }

        [HttpGet("servererror")] // Get: /api/Buggy/servererror
        public async Task<IActionResult> GetServerError()
        {
            var rooms = await _context.Rooms.FindAsync(100);
            var roomToString = rooms.ToString(); // will through exception
            return Ok(rooms);
        }

        [HttpGet("badrequest")] // Get: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badrequest/{id}")] // Get: /api/Buggy/badrequest/kholoud
        public async Task<IActionResult> GetBadRequestError(int id) // ValidationError
        {
            return Ok();
        }

        [HttpGet("unauthrized")] // Get: /api/Buggy/unauthrized
        public async Task<IActionResult> GetUnauthorizedError(int id)
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
