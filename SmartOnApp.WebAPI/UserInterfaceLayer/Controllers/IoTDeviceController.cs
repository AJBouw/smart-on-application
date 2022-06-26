using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTDeviceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILdrRepository _ldrRepository;
        private readonly ILogger<IoTDeviceController> _logger;
        private readonly IMapper _mapper;

        public IoTDeviceController(IUnitOfWork unitOfWork,
            ILdrRepository ldrRepository,
            ILogger<IoTDeviceController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ldrRepository = ldrRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllIoTDeviceInclude")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllIoTDeviceInclude()
        {
            try
            {
                var allIoTDeviceInclude = await _unitOfWork.iot_device.GetAllAsync(include: y => y.Include(x => x.Mcu));
                var result = _mapper.Map<IList<IoTDeviceDTO>>(allIoTDeviceInclude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLdrIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("ldr/all/{macAddress}", Name = "GetAllLdrIncludeByMacAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLdrIncludeByMacAddress(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
            {
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var allLdrIncludeByMacAddress = await _unitOfWork.ldr.GetAllAsync(x => x.Mcu.McuMacAddress == macAddress, include: y => y.Include(x => x.Mcu));
                var result = _mapper.Map<IList<LdrDTO>>(allLdrIncludeByMacAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLdrIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("ldr/{id:int}", Name = "GetLdrIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLdrIncludeById(int id)
        {
            try
            {
                var ldr = await _unitOfWork.ldr.GetAsync(x => x.Id == id, include: y => y.Include(x => x.Mcu));
                var result = _mapper.Map<LdrDTO>(ldr);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLdrIncludeById)}");
                return StatusCode(500, "Internal server errror. Please, try again later.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLdr([FromBody] CreateLdrDTO ldrDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP POST request in {nameof(CreateLdr)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ldr = _mapper.Map<Ldr>(ldrDTO);
                await _unitOfWork.ldr.InsertAsync(ldr);
                await _unitOfWork.SaveAsync();
                return CreatedAtRoute("GetLdrIncludeById", new { id = ldr.Id }, ldr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateLdr)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}
