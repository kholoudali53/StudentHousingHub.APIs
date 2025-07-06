using Microsoft.AspNetCore.Http;
using StudentHousingHub.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Service.Services.Users
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        }
    }
}
