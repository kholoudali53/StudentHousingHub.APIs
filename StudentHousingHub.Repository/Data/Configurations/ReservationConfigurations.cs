using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository.Data.Configurations
{
    public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Primary Key
            builder.HasKey(r => r.id);

            // Property configurations
            builder.Property(r => r.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.LastName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.PhoneNo).IsRequired().HasMaxLength(20);
            builder.Property(r => r.NationalId).IsRequired().HasMaxLength(14).IsFixedLength();
            builder.Property(r => r.PictureUrl).HasMaxLength(500);
            builder.Property(r => r.Status).HasConversion<string>();

            // Relationships
            builder.HasOne(r => r.Apartment)
                .WithMany(a => a.Reservations)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Bed)
           .WithMany()
           .HasForeignKey(r => r.BedId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Student)
                .WithMany()
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Constrains
            builder.HasCheckConstraint("CK_Reservations_Dates", "[CheckOutDate] > [CheckInDate]");
            builder.HasCheckConstraint("CK_Reservations_RoomNumber", "[RoomNumber] BETWEEN 1 AND 10");

        }
    }
}
