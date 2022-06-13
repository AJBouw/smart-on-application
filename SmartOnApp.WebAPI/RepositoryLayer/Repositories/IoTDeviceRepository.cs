using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
	public class IoTDeviceRepository : GenericRepository<IoTDevice>, IIoTDeviceRepository
	{
		private readonly SmartOnDbContext _db;

		public IoTDeviceRepository(SmartOnDbContext db) : base(db)
		{
			_db = db;
		}
	}
}

