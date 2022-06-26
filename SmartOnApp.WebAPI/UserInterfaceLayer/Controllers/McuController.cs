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

        public McuController(IUnitOfWork unitOfWork,
            IMcuRepository mcuRepository,
            ILogger<McuController> logger,
            IMapper mapper)
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
                var allMcu = await _unitOfWork.mcu.GetAllAsync();
                var result = _mapper.Map<IList<McuDTO>>(allMcu);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMcuInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("{macAddress}", Name = "GetMcuIncludeByMacAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMcuIncludeByMacAddress(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
            {
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var allMcuInclude = await _unitOfWork.mcu.GetAsync(x => x.McuMacAddress == macAddress, include: y => y.Include(x => x.IoTDevices));
                var result = _mapper.Map<McuDTO>(allMcuInclude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMcuIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMcu([FromBody] CreateMcuDTO mcuDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP POST request in {nameof(CreateMcu)}");
                return BadRequest(ModelState);
            }

            try
            {
                var mcu = _mapper.Map<Mcu>(mcuDTO);
                await _unitOfWork.mcu.InsertAsync(mcu);
                await _unitOfWork.SaveAsync();
                return CreatedAtRoute("GetMcuIncludeByMacAddress", new { macAddress = mcu.McuMacAddress }, mcu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateMcu)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}
