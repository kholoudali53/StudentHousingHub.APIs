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
    public class ContactConfigurations : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            // Primary Key
            builder.HasKey(c => c.id);

            builder.Property(c => c.CommunicationType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.ChatOnWhatsapp)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.EmailUs)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CallUs)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            // Relationships
            builder.HasOne(c => c.Owner)
           .WithMany(o => o.SentMessages)
           .HasForeignKey(c => c.OwnerId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Student)
            .WithMany(s => s.ReceivedMessages)
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
