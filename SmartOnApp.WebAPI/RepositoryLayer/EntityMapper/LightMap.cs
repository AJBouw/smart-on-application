﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityMapper
{
    public class LightMap : IEntityTypeConfiguration<Light>
    {

        public void Configure(EntityTypeBuilder<Light> builder)
        {
            builder.ToTable("light");
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("light_id")
                .HasColumnType("INT");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.LightIsOn)
                .HasColumnName("light_is_on");
            builder.HasOne(x => x.IoTDevice)
                .WithMany(y => y.Lights)
                .HasForeignKey(x => x.IoTDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
