using Microsoft.AspNetCore.Identity;
using StudentHousingHub.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            var user = new AppUser()
            {
                DisplayName = "Kholoud Ali",
                UserName = "kholoud.ali",
                PhoneNumber = "1234567890",
                Email = "kholoudali@gmail.com"
            };

            await _userManager.CreateAsync(user, "P@ssw0rds");
        }
    }
}
