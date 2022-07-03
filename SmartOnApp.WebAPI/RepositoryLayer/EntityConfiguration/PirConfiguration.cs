﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class PirConfiguration : IEntityTypeConfiguration<Pir>
    {
        public void Configure(EntityTypeBuilder<Pir> builder)
        {
            builder.ToTable("pir");
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("pir_id");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.MotionIsDetected)
                .HasColumnName("motion_is_detected");
            builder.HasOne(x => x.IoTDevice)
                .WithMany(y => y.Pirs)
                .HasForeignKey(y => y.IoTDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
