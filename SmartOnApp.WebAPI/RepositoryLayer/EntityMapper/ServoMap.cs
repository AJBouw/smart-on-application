using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityMapper
{
    public class ServoMap : IEntityTypeConfiguration<Servo>
    {
        public void Configure(EntityTypeBuilder<Servo> builder)
        {
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp");
            builder.Property(x => x.ServoCurrentPosition)
                .HasColumnName("servo_current_position")
                .HasColumnName("INT");
        }
    }
}
