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
    public class OwnerConfigurations : IEntityTypeConfiguration<Owners>
    {
        public void Configure(EntityTypeBuilder<Owners> builder)
        {
            // Primary Key
            builder.HasKey(o => o.id);

            // Property configurations
            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(o => o.NationalID)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnType("char(14)")
                .HasConversion(
                    v => v,
                    v => v.Trim());

            builder.Property(o => o.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");

            builder.Property(o => o.DOB)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(o => o.Phone_NO)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("varchar(15)")
                .HasColumnName("PhoneNumber");


            builder.HasIndex(o => o.NationalID).IsUnique();

            builder.HasIndex(o => o.Email).IsUnique();

            builder.HasIndex(o => o.Phone_NO).IsUnique();




                 // Relationships
                 builder.HasMany(o => o.Rooms)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

                 builder.HasMany(o => o.Reports)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

                 builder.HasMany(o => o.SentMessages)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            // Check constraints
            builder.HasCheckConstraint("CK_Owners_Gender", "[Gender] IN ('Male', 'Female', 'Other')");


        }
    }
}
