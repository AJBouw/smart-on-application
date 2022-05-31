using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityMapper
{
    public class PirMap : IEntityTypeConfiguration<Pir>
    {
        public void Configure(EntityTypeBuilder<Pir> builder)
        {
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.MotionIsDetected)
                .HasColumnName("motion_is_detected");
        }
    }
}
