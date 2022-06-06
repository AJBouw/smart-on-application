using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration
{
    public class LightConfiguration : IEntityTypeConfiguration<Light>
    {

        public void Configure(EntityTypeBuilder<Light> builder)
        {
            builder.ToTable("light");
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.LightIsOn)
                .HasColumnName("light_is_on");
        }
    }
}
