using System;
using SmartOnApp.Shared.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.WebAPI.RepositoryLayer.EntityConfiguration;

namespace SmartOnApp.WebAPI.RepositoryLayer
{
    public class SmartOnDbContext : DbContext
    {
        public DbSet<IoTDevice> iot_device { get; set; }
        public DbSet<Ldr> ldr { get; set; }
        public DbSet<Light> light { get; set; }
        public DbSet<Mcu> mcu { get; set; }
        public DbSet<Pir> pir { get; set; }
        public DbSet<Servo> servo { get; set; }

        public SmartOnDbContext(DbContextOptions<SmartOnDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IoTDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new LdrConfiguration());
            modelBuilder.ApplyConfiguration(new LightConfiguration());
            modelBuilder.ApplyConfiguration(new McuConfiguration());
            modelBuilder.ApplyConfiguration(new PirConfiguration());
            modelBuilder.ApplyConfiguration(new ServoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
