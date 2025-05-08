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
    public class RoomConfigurations : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.HasKey(r => r.id);

            builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("nvarchar(20)");

            builder.Property(r => r.TotalBeds)
                .IsRequired();

            builder.Property(r => r.AvailableBeds)
                .IsRequired();

            builder.Property(r => r.PricePerBed)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Check constraints
            builder.HasCheckConstraint("CK_Rooms_AvailableBeds", "[AvailableBeds] BETWEEN 0 AND [TotalBeds]");
            builder.HasCheckConstraint("CK_Rooms_TotalBeds", "[TotalBeds] BETWEEN 1 AND 10");

            // Relationship with Beds
            builder.HasMany(r => r.Beds)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
