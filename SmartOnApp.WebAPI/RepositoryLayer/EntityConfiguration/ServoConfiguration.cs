using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class ServoConfiguration : IEntityTypeConfiguration<Servo>
    {
        public void Configure(EntityTypeBuilder<Servo> builder)
        {
            builder.ToTable("servo");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.ServoCurrentPosition)
                .HasColumnName("servo_current_position")
                .HasColumnType("INT");
        }
    }
}
