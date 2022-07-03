using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class LdrConfiguration : IEntityTypeConfiguration<Ldr>
    {
        public void Configure(EntityTypeBuilder<Ldr> builder)
        {
            builder.ToTable("ldr");
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ldr_id");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.Brightness)
                .HasColumnName("brightness");
            builder.Property(x => x.IoTDeviceId)
                .HasColumnName("iot_device_id");
            builder.HasOne(x => x.IoTDevice)
                .WithMany(y => y.Ldrs)
                .HasForeignKey(y => y.IoTDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
