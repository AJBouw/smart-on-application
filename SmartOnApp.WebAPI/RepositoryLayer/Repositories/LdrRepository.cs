﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.RepositoryLayer.Repositories
{
    public class LdrRepository : GenericRepository<Ldr>, ILdrRepository
    {
        private readonly SmartOnDbContext _db;

        public LdrRepository(SmartOnDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
