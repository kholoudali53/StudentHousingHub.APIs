using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentHousingHub.APIs.Middlewares;
using StudentHousingHub.Core.Exceptions;
using StudentHousingHub.Core.Identity;
using StudentHousingHub.Repository.Data.Contexts;
using StudentHousingHub.Repository.Identity;
using StudentHousingHub.Repository.Identity.Contexts;

namespace StudentHousingHub.APIs.Helper
{
    public static class ConfigureMiddleWare
    {
        public static async Task<WebApplication> ConfigureMiddleWareAsync(this WebApplication app)
        {
            // update database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>(); // Ask CLR Create Object AppDbContext
            var identityContext = services.GetRequiredService<AppIdentityDbContext>(); // // Ask CLR Create Object AppIdentityDbContext
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // LoggerFactory: object clr Created it
            try
            {
                await context.Database.MigrateAsync(); // update database

                //await AppDbContextSeed.SeedAsync(context);

                await identityContext.Database.MigrateAsync();

                await AppIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "There are problem during apply migration !");
            }

            app.UseMiddleware<ExceptionMiddleWare>(); // Configure User-Defined [ExceptionMidleWare] Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var statusCode = exception switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
            }));

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            return app;
        }
    }
}
