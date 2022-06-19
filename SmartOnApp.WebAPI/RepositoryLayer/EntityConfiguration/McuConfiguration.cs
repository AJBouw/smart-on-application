using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class McuConfiguration : IEntityTypeConfiguration<Mcu>
    {
        public void Configure(EntityTypeBuilder<Mcu> builder)
        {
            builder.ToTable("mcu");
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("mcu_id");
            builder.Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasColumnName("created_at")
                .HasColumnType("datetime");
            builder.Property(x => x.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updated_at")
                .HasColumnType("datetime");
            builder.Property(x => x.McuName)
                .HasColumnName("mcu_name");
            builder.Property(x => x.McuMacAddress)
                .HasColumnName("mcu_mac_address");
            builder.Property(x => x.McuIp)
                .HasColumnName("mcu_ip");
            builder.Property(x => x.McuHostname)
                .HasColumnName("mcu_hostname");
            builder.Property(x => x.Location)
                .HasColumnName("location");
        }
    }
}
