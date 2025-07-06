using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHousingHub.Core.Dtos.Reports;
using StudentHousingHub.Core.Services.Interface;
using System.Security.Claims;

namespace StudentHousingHub.APIs.Controllers
{
    public class ReportsController : BaseApiController
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")] // Only admins can view all reports
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> GetReportById(int id)
        {
            try
            {
                var report = await _reportService.GetReportByIdAsync(id);
                return Ok(report);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<ReportDto>> CreateReport([FromBody] CreateReportDto createReportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reportDto = await _reportService.CreateReportAsync(createReportDto);
                return CreatedAtAction(nameof(GetReportById), new { id = reportDto.id }, reportDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    Error = "Authorization failed",
                    Details = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")] // Only admins can delete reports
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                await _reportService.DeleteReportAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost("{id}/handle")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> HandleReport(int id)
        //{
        //    await _reportService.HandleReportAsync(id);
        //    return NoContent();
        //}
    }
}
