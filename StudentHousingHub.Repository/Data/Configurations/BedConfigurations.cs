using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository.Data.Configurations
{
    public class BedConfigurations : IEntityTypeConfiguration<Beds>
    {
        public void Configure(EntityTypeBuilder<Beds> builder)
        {
            builder.HasKey(b => b.id);

            builder.Property(b => b.BedNumber)
            .IsRequired()
            .HasMaxLength(20);

            builder.Property(b => b.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            // Relationship with Reservation
            builder.HasOne<Reservation>()
                .WithOne(r => r.Bed)
                .HasForeignKey<Reservation>(r => r.BedId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
