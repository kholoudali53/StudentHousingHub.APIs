using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentHousingHub.Core;
using StudentHousingHub.Core.Dtos.Reports;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Exceptions;
using StudentHousingHub.Core.Identity;
using StudentHousingHub.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Service.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
        {
            var reports = await _unitOfWork.Repository<Core.Entities.Reports, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }

        public async Task<ReportDto> GetReportByIdAsync(int id)
        {
            var report = await _unitOfWork.Repository<Core.Entities.Reports, int>().GetAsync(id);
            if (report == null)
            {
                throw new NotFoundException($"Report with ID {id} not found");
            }
            return _mapper.Map<ReportDto>(report);
        }


        public async Task DeleteReportAsync(int id)
        {
            var report = await _unitOfWork.Repository<Core.Entities.Reports, int>().GetAsync(id);
            if (report == null)
            {
                throw new NotFoundException($"Report with ID {id} not found");
            }

            _unitOfWork.Repository<Core.Entities.Reports, int>().Delete(report);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ReportDto> CreateReportAsync(CreateReportDto ReportDto)
        {
            var currentUserEmail = _currentUserService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
            {
                throw new UnauthorizedAccessException("User email not found in claims");
            }

            var currentUser = await _unitOfWork.Repository<ApppUserr, string>()
                .GetByIdAsync(currentUserEmail,
                include: q => q.Include(u => u.Student)
                .Include(u => u.Owner));

            if (currentUser == null)
            {
                throw new UnauthorizedAccessException($"User with email {currentUserEmail} not found");
            }

            if (currentUser.Student == null)
                throw new InvalidOperationException("User is not associated with a student");

            if (currentUser.Owner == null)
                throw new InvalidOperationException("User is not associated with an owner");

            var admins = await _unitOfWork.Repository<Admin, int>().GetAllAsync();
            var admin = admins.FirstOrDefault();

            // Create new report
            var report = new Core.Entities.Reports
            {
                Problem = ReportDto.Problem,
                StudentId = currentUser.Student.id,
                OwnerId = currentUser.Owner.id,
                AdminId = admin?.id ?? 0, // Default to 0 if no admin exists
                CreateAt = DateTime.UtcNow
            };

            // Add report to database
            await _unitOfWork.Repository<Core.Entities.Reports, int>().AddAsync(report);
            await _unitOfWork.CompleteAsync();

            // Load related data for the DTO
            var reportWithRelations = await _unitOfWork.Repository<Core.Entities.Reports, int>()
                .GetByIdAsync(report.id,
                    include: q => q.Include(r => r.Student)
                                  .Include(r => r.Owner)
                                  .Include(r => r.Admin));

            // Map to DTO
            return new ReportDto
            {
                id = reportWithRelations.id,
                Problem = reportWithRelations.Problem,
                StudentId = reportWithRelations.StudentId,
                OwnerId = reportWithRelations.OwnerId,
                AdminId = reportWithRelations.AdminId,
                CreateAt = reportWithRelations.CreateAt
            };
        }
    }


        //public async Task HandleReportAsync(int id)
        //{
        //    var report = await _unitOfWork.Repository<Core.Entities.Reports, int>().GetAsync(id);
        //    if (report == null)
        //    {
        //        throw new NotFoundException($"Report with ID {id} not found");
        //    }

        //    report.IsHandled = true;
        //    report.HandledDate = DateTime.UtcNow;

        //    _unitOfWork.Repository<Core.Entities.Reports, int>().Update(report);
        //    await _unitOfWork.CompleteAsync();
        //}
    }