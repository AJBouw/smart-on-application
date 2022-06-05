using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTDeviceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILdrRepository _ldrRepository;

        public IoTDeviceController(IUnitOfWork unitOfWork, ILdrRepository ldrRepository)
        {
            _unitOfWork = unitOfWork;
            _ldrRepository = ldrRepository;
        }

        [HttpGet("ldr/{macAddress}")]
        public async Task<IEnumerable<Ldr>> GetAllLdrIncludeByMacAddress(string macAddress)
        {
            return await _ldrRepository.GetAllLdrIncludeByMacAddressAsync(macAddress);
        }

    }
}
