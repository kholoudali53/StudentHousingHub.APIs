using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentHousingHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;

namespace StudentHousingHub.Repository.Data.Configurations
{
    public class ApartmentConfigurations : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(r => r.id);

            // Property configurations
            builder.Property(a => a.UniversityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");

            builder.Property(a => a.Space)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(a => a.Floor)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(2000);

            builder.Property(a => a.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Images (List<string>)
            // Images list serialization with value comparer
            builder.Property(a => a.Images)
                .HasConversion(
                    new ValueConverter<List<string>, string>(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()))
                .HasColumnType("nvarchar(max)");

            // Check constraints
            builder.HasCheckConstraint("CK_Apartments_Gender", "[Gender] IN ('Male', 'Female', 'Other')");
            builder.HasCheckConstraint("CK_Apartments_Floor", "[Floor] BETWEEN 0 AND 50");

            // Relationships
            builder.HasOne(a => a.Owner)
                .WithMany()
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Rooms)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Reservations)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
