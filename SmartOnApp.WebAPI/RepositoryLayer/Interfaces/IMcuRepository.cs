using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartOnApp.Shared.DomainLayer.Models;

namespace SmartOnApp.WebAPI.RepositoryLayer.Interfaces
{
    public interface IMcuRepository
    {
        Task<IList<Mcu>> GetAllMcuIncludeAsync();
    }
}
