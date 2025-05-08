using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHousingHub.APIs.Errors;

namespace StudentHousingHub.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found End Point"));
        }
    }
}
