using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class PirRepository : Repository<Pir>, IPirRepository
    {
        private readonly SmartOnDbContext _db;

        public PirRepository(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
