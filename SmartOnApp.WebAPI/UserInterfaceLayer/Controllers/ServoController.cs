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
    public class ServoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServoRepository _servoRepository;
        private readonly ILogger<ServoController> _logger;
        private readonly IMapper _mapper;

        public ServoController(IUnitOfWork unitOfWork,
            IServoRepository servoRepository,
            ILogger<ServoController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _servoRepository = servoRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("servo/all/{macAddress}", Name = "GetAllServoIncludeByMacAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllServoIncludeByMacAddress(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
            {
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var allServoIncludeByMacAddress = await _unitOfWork.servo.GetAllAsync(x => x.IoTDevice.Mcu.McuMacAddress == macAddress, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<IList<ServoDTO>>(allServoIncludeByMacAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllServoIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("servo/{id:int}", Name = "GetServoIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServoIncludeById(int id)
        {
            try
            {
                var servo = await _unitOfWork.servo.GetAsync(x => x.Id == id, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<ServoDTO>(servo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetServoIncludeById)}");
                return StatusCode(500, "Internal server errror. Please, try again later.");
            }
        }

        [HttpPost("servo")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertServo([FromBody] CreateServoDTO servoDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(InsertServo)}");
                return BadRequest(ModelState);
            }

            try
            {
                var servo = _mapper.Map<Servo>(servoDTO);

                await _unitOfWork.servo.InsertAsync(servo);
                await _unitOfWork.SaveAsync();
                return CreatedAtRoute("GetServoIncludeById", new { id = servo.Id }, servo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(InsertServo)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }

}

