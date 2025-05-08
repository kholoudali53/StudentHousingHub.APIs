using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Services.Interface
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string Key, object response, TimeSpan expireTime);
        Task<String> GetCacheKeyAsync(string Key);
    }
}
