using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class LightRepository : GenericRepository<Light>, ILightRepository
    {
        private readonly SmartOnDbContext _db;

        public LightRepository(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
