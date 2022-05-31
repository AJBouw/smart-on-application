﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityMapper
{
    public class McuMap : IEntityTypeConfiguration<Mcu>
    {
        public void Configure(EntityTypeBuilder<Mcu> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("INT");
            builder.Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasColumnName("created_at")
                .HasColumnType("datetime");
            builder.Property(x => x.UpdatedAt)
                .ValueGeneratedOnUpdate()
                .HasColumnName("updated_at")
                .HasColumnType("datetime");
            builder.Property(x => x.McuName)
                .HasColumnName("mcu_name")
                .HasColumnType("VARCHAR(30)");
            builder.Property(x => x.McuMacAddress)
                .HasColumnName("mcu_mac_address");
            builder.Property(x => x.McuIp)
                .HasColumnName("mcu_ip")
                .HasColumnType("VARCHAR(20)");
            builder.Property(x => x.McuHostname)
                .HasColumnName("mcu_hostname")
                .HasColumnType("VARCHAR(50)");
            builder.Property(x => x.McuName)
                .HasColumnName("location")
                .HasColumnType("VARCHAR(50)");
        }
    }
}
