using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMcuRepository _mcuRepository;
        private readonly ILogger<McuController> _logger;
        private readonly IMapper _mapper;

        public McuController(IUnitOfWork unitOfWork, IMcuRepository mcuRepository, ILogger<McuController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mcuRepository = mcuRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMcuInclude()
        {
            try
            {
                var allMcu = await _mcuRepository.GetAllMcuIncludeAsync();
                var result = _mapper.Map<IList<McuDTO>>(allMcu);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMcuInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }

        }

        [HttpGet("{macAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMcuIncludeByMacAddress(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
            {
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var allMcuInclude = await _unitOfWork.mcu.GetAsync(x => x.McuMacAddress == macAddress, new List<string> { "IoTDevices" });
                var result = _mapper.Map<McuDTO>(allMcuInclude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMcuIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }

        }
    }
}
