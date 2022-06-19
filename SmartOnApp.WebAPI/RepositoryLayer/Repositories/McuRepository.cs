using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class McuRepository : IMcuRepository
    {
        private readonly SmartOnDbContext _db;

        public McuRepository(SmartOnDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Mcu>> GetAllMcuIncludeAsync()
        {
            return await _db.mcu
                .Include(x => x.IoTDevices)
                .ToListAsync();
        } 
    }
}
