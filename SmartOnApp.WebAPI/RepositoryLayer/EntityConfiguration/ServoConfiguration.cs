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
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("servo_id");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.ServoCurrentPosition)
                .HasColumnName("servo_current_position");
            builder.Property(x => x.IoTDeviceId)
                .HasColumnName("iot_device_id");
            builder.HasOne(x => x.IoTDevice)
                .WithMany(y => y.Servos)
                .HasForeignKey(y => y.IoTDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
