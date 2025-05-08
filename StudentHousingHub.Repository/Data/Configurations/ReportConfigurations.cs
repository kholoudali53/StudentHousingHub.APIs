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
    public class ReportsConfigurations : IEntityTypeConfiguration<Reports>
    {
        public void Configure(EntityTypeBuilder<Reports> builder)
        {
            builder.HasKey(r => r.id);

            builder.Property(r => r.Problem)
                .IsRequired()
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000);

            // Relationships

            // Student relationship (many-to-one)
            builder.HasOne(r => r.Student)
                .WithMany(s => s.Reports)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Owner relationship (many-to-one)
            builder.HasOne(r => r.Owner)
                .WithMany(o => o.Reports)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Admin relationship (many-to-one - optional)
            builder.HasOne(r => r.Admin)
            .WithMany(a => a.Reports)
            .HasForeignKey(r => r.AdminId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
