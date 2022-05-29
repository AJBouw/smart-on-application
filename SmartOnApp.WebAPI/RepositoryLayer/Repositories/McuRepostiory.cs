using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class McuRepostiory : Repository<Mcu>, IMcuRepository
    {
        private readonly SmartOnDbContext _db;
        
        public McuRepostiory(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
