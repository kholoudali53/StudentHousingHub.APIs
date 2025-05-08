
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentHousingHub.APIs.Errors;
using StudentHousingHub.APIs.Helper;
using StudentHousingHub.APIs.Middlewares;
using StudentHousingHub.Core;
using StudentHousingHub.Core.Services.Interface;
using StudentHousingHub.Repository;
using StudentHousingHub.Repository.Data.Contexts;
using StudentHousingHub.Service.Services.Rooms;

namespace StudentHousingHub.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            var app = builder.Build();

            await app.ConfigureMiddleWareAsync();

            app.Run();
        }
    }
}
