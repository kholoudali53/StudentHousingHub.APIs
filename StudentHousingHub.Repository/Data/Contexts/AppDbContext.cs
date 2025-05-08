using Microsoft.EntityFrameworkCore;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        public DbSet<Reports> Reports { get; set; }
    }
}
