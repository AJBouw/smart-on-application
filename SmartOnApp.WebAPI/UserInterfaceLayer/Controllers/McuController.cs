using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McuController : ControllerBase
    {
        private readonly IMcuRepository _mcuRepository;

        public McuController(IMcuRepository mcuRepository) => _mcuRepository = mcuRepository;

        [HttpGet]
        public async Task<IActionResult> GetMcus() => Ok(value: await _mcuRepository.GetAll());
    }
}
