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
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp")
                .HasColumnType("datetime");
            builder.Property(x => x.Brightness)
                .HasColumnName("brightness")
                .HasColumnType("INT");
        }
    }
}
