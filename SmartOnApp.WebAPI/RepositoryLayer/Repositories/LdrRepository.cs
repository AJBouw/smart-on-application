using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class LdrRepository : Repository<Ldr>, ILdrRepository
    {
        private readonly SmartOnDbContext _db;

        public LdrRepository(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
