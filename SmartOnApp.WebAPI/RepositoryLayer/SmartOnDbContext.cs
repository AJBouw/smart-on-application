using System;
using SmartOnApp.Shared.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.WebAPI.RepositoryLayer.EntityMapper;

namespace SmartOnApp.WebAPI.RepositoryLayer
{
    public class SmartOnDbContext : DbContext
    {
        public DbSet<IoTDevice> IoTDevices { get; set; }
        public DbSet<Mcu> Mcus { get; set; }
        public DbSet<Ldr> Ldrs { get; set; }
        public DbSet<Light> Lights { get; set; }
        public DbSet<Pir> Pirs { get; set; }
        public DbSet<Servo> Servos { get; set; }

        public SmartOnDbContext(DbContextOptions<SmartOnDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IoTDeviceMap());
            modelBuilder.ApplyConfiguration(new LdrMap());
            modelBuilder.ApplyConfiguration(new LightMap());
            modelBuilder.ApplyConfiguration(new McuMap());
            modelBuilder.ApplyConfiguration(new PirMap());
            modelBuilder.ApplyConfiguration(new ServoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
