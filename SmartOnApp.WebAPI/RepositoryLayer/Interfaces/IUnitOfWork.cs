using System;
using System.Threading.Tasks;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<IoTDevice> iot_device { get; }
        IGenericRepository<Ldr> ldr { get; }
        IGenericRepository<Light> light { get; }
        IGenericRepository<Mcu> mcu { get; }
        IGenericRepository<Pir> pir { get; }
        IGenericRepository<Servo> servo { get; }

        Task Save();
    }
}
