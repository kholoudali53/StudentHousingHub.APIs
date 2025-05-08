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
    public class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.id);

            // Property configurations
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.NationalID)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength(); // For char(14) in SQL

            builder.Property(a => a.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.DOB)
                .IsRequired()
                .HasColumnType("date"); // Maps to SQL date type

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(a => a.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");



            // Relationships
            builder.HasMany(a => a.Owners)
            .WithOne(o => o.Admin)
            .HasForeignKey(o => o.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Students)
                .WithOne(s => s.Admin)
                .HasForeignKey(s => s.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Reports)
                .WithOne(r => r.Admin)
                .HasForeignKey(r => r.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
