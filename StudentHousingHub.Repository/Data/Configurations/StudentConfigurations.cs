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
    public class StudentConfigurations : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            // Primary Key
            builder.HasKey(s => s.id);

            // Property configurations
            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(s => s.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");

            builder.Property(s => s.NationalID)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnType("varchar(14)")
                .IsFixedLength();

            builder.Property(s => s.DOB)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(s => s.PhoneNo)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("varchar(15)");


            // Indexes
            builder.HasIndex(s => s.NationalID).IsUnique();
            builder.HasIndex(s => s.Email).IsUnique();
            builder.HasIndex(s => s.PhoneNo).IsUnique();
            builder.HasIndex(s => s.AdminId);


            // Relationships
            // Admin relationship (many-to-one)
            builder.HasOne(s => s.Admin)
                .WithMany(a => a.Students)
                .HasForeignKey(s => s.AdminId)
                .OnDelete(DeleteBehavior.Restrict);


            // Reports relationship (one-to-many)
            builder.HasMany(s => s.Reports)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ContactUs relationship (one-to-many)
            builder.HasMany(s => s.ReceivedMessages)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Check constraints
            builder.HasCheckConstraint("CK_Students_Gender", "[Gender] IN ('Male', 'Female', 'Other')");


        }
    }
}
