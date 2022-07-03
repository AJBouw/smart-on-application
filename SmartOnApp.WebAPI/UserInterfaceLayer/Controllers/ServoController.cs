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

        [HttpGet("all", Name = "GetAllServoInclude")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllServoInclude()
        {
            try
            {
                var allServo = await _unitOfWork.servo.GetAllAsync(include: y => y.Include(x => x.IoTDevice).ThenInclude(z => z.Mcu));
                var result = _mapper.Map<IList<ServoDTO>>(allServo);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllServoInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetServoIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServoIncludeById(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP GET request in {nameof(GetServoIncludeById)}" +
                    $"{id} is invalid");
                return BadRequest("Invalid id");
            }

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

        [HttpPost()]
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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateServo(int id, [FromBody] UpdateServoDTO servoDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateServo)}");
                return BadRequest(ModelState);
            }

            try
            {
                var servo = await _unitOfWork.servo.GetAsync(x => x.Id == id);

                if (servo == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateServo)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(servoDTO, servo);
                _unitOfWork.servo.UpdateAsync(servo);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateServo)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteServo(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeleteServo)}");
                return BadRequest(ModelState);
            }

            try
            {
                var servo = await _unitOfWork.servo.GetAsync(x => x.Id == id);

                if (servo == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeleteServo)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.ldr.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteServo)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}