using System;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class ServoRepository : GenericRepository<Servo>, IServoRepository
    {
        private readonly SmartOnDbContext _db;

        public ServoRepository(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
