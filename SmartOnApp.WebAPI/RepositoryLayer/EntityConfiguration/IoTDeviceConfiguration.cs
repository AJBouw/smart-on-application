﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
    {
        public void Configure(EntityTypeBuilder<IoTDevice> builder)
        {
            builder.ToTable("iot_device");
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("iot_device_id");
            builder.Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasColumnName("created_at")
                .HasColumnType("datetime");
            builder.Property(x => x.UpdatedAt)
                .ValueGeneratedOnUpdate()
                .HasColumnName("updated_at")
                .HasColumnType("datetime");
            builder.Property(x => x.IoTDeviceName)
                .HasColumnName("iot_device_name");
            builder.Property(x => x.IoTDeviceType)
                .HasColumnName("iot_device_type");
            builder.Property(x => x.McuId)
                .HasColumnName("mcu_id");
            builder.HasOne(x => x.Mcu)
                .WithMany(y => y.IoTDevices)
                .HasForeignKey(y => y.McuId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
