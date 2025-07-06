using StudentHousingHub.Core.Dtos.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Services.Interface
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllReportsAsync();
        Task<ReportDto> GetReportByIdAsync(int id);
        Task DeleteReportAsync(int id);
        Task<ReportDto> CreateReportAsync(CreateReportDto ReportDto);
        //Task HandleReportAsync(int id);
    }
}
