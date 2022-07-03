using System;
using System.Collections.Generic;
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
    public class LightController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILightRepository _lightRepository;
        private readonly ILogger<LightController> _logger;
        private readonly IMapper _mapper;

        public LightController(IUnitOfWork unitOfWork,
            ILightRepository lightRepository,
            ILogger<LightController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _lightRepository = lightRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("light/all/{macAddress}", Name = "GetAllLightIncludeByMacAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLightIncludeByMacAddress(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
            {
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var allLightIncludeByMacAddress = await _unitOfWork.light.GetAllAsync(x => x.IoTDevice.Mcu.McuMacAddress == macAddress, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<IList<LightDTO>>(allLightIncludeByMacAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLightIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("light/{id:int}", Name = "GetLightIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLightIncludeById(int id)
        {
            try
            {
                var light = await _unitOfWork.light.GetAsync(x => x.Id == id, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<LightDTO>(light);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLightIncludeById)}");
                return StatusCode(500, "Internal server errror. Please, try again later.");
            }
        }

        [HttpPost("light")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertLight([FromBody] CreateLightDTO lightDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(InsertLight)}");
                return BadRequest(ModelState);
            }

            try
            {
                var light = _mapper.Map<Light>(lightDTO);

                await _unitOfWork.light.InsertAsync(light);
                await _unitOfWork.SaveAsync();
                return CreatedAtRoute("GetLightIncludeById", new { id = light.Id }, light);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(InsertLight)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}

