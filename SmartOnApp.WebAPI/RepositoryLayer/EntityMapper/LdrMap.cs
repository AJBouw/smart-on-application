using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.EntityMapper
{
    public class LdrMap : IEntityTypeConfiguration<Ldr>
    {
        public void Configure(EntityTypeBuilder<Ldr> builder)
        {
            builder.Property(x => x.Timestamp)
                .ValueGeneratedOnAdd()
                .HasColumnName("timestamp");
            builder.Property(x => x.Brightness)
                .HasColumnName("brightness")
                .HasColumnType("INT");
        }
    }
}
