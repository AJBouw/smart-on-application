using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMcuRepository _mcuRepository;

        public McuController(IUnitOfWork unitOfWork, IMcuRepository mcuRepository)
        {
            _unitOfWork = unitOfWork;
            _mcuRepository = mcuRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Mcu>> GetAllMcu()
        {
            return await _unitOfWork.mcu.GetAllAsync();
        }

        [HttpGet("include")]
        public async Task<IEnumerable<Mcu>> GetAllMcuInclude()
        {
            return await _mcuRepository.GetAllMcuIncludeAsync();
        }
    }
}
