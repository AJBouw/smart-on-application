using System;
using System.Threading.Tasks;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartOnDbContext _smartOnDbContext;
        private IGenericRepository<IoTDevice> _iotDevice;
        private IGenericRepository<Ldr> _ldr;
        private IGenericRepository<Light> _light;
        private IGenericRepository<Mcu> _mcu;
        private IGenericRepository<Pir> _pir;
        private IGenericRepository<Servo> _servo;

        public UnitOfWork(SmartOnDbContext smartOnDbContext)
        {
            _smartOnDbContext = smartOnDbContext;
        }

        public IGenericRepository<IoTDevice> iot_device => _iotDevice ??= new GenericRepository<IoTDevice>(_smartOnDbContext);
        public IGenericRepository<Ldr> ldr => _ldr ??= new GenericRepository<Ldr>(_smartOnDbContext);
        public IGenericRepository<Light> light => _light ??= new GenericRepository<Light>(_smartOnDbContext);
        public IGenericRepository<Mcu> mcu => _mcu ??= new GenericRepository<Mcu>(_smartOnDbContext);
        public IGenericRepository<Pir> pir => _pir ??= new GenericRepository<Pir>(_smartOnDbContext);
        public IGenericRepository<Servo> servo => _servo ??= new GenericRepository<Servo>(_smartOnDbContext);

        public void Dispose()
        {
            _smartOnDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _smartOnDbContext.SaveChangesAsync();
        }
    }
}
