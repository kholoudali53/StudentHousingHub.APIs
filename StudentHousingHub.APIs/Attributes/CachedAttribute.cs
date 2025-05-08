using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using StudentHousingHub.Core.Services.Interface;

namespace StudentHousingHub.APIs.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CachedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var casheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var casheResponse = await casheService.GetCacheKeyAsync(casheKey);

            if (!string.IsNullOrEmpty(casheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = casheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response)
            {
                await casheService.SetCacheKeyAsync(casheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
            }

        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            var casheKey = new StringBuilder();
            casheKey.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(X => X.Key))
            {
                casheKey.Append($"|{key}-{value}");
            }
            return casheKey.ToString();
        }
    }
}