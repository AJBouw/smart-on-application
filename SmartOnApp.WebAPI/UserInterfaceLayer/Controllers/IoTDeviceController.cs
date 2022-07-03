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

        // TODO include ioTDevices with its ldrs, lights, pirs, servos ??
        [HttpGet("all", Name = "GetAllIoTDeviceInclude")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllIoTDeviceInclude()
        {
            try
            {
                var allIoTDevice = await _unitOfWork.iot_device.GetAllAsync(include:
                    y => y.Include(x => x.Mcu));
                var result = _mapper.Map<IList<IoTDeviceDTO>>(allIoTDevice);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllIoTDeviceInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        // TODO include ldrs, lights, pirs, servos ??
        [HttpGet("{id:int}", Name = "GetIoTDeviceIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIoTDeviceIncludeById(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP GET request in {nameof(GetIoTDeviceIncludeById)}" +
                    $"{id} is invalid");
                return BadRequest("Invalid id");
            }

            try
            {
                var ioTDevice = await _unitOfWork.iot_device.GetAsync(x => x.Id == id, include: y => y.Include(x => x.Mcu));
                var result = _mapper.Map<IoTDevice>(ioTDevice);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetIoTDeviceIncludeById)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        // TODO include ldrs, lights, pirs, servos ??
        [HttpGet("{name}", Name = "GetIoTDeviceByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIoTDeviceIncludeByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Invalid HTTP GET request in {nameof(GetIoTDeviceIncludeByName)}");
                return new BadRequestObjectResult("MAC address must not be null or empty");
            }

            try
            {
                var ioTDevice = await _unitOfWork.iot_device.GetAsync(x => x.IoTDeviceName == name, include: y => y.Include(x => x.Mcu));
                var result = _mapper.Map<IoTDevice>(ioTDevice);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetIoTDeviceIncludeByName)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertIoTDevice([FromBody] CreateIoTDeviceDTO ioTDeviceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(InsertIoTDevice)}");
                return BadRequest(ModelState);
            }

            try
            {
                var validateIoTDevice = await _unitOfWork.iot_device.GetAsync(x => x.IoTDeviceName == ioTDeviceDTO.IoTDeviceName);

                if (validateIoTDevice != null)
                {
                    _logger.LogError($"Invalid IoTDevice name. {ioTDeviceDTO.IoTDeviceName} already exists");
                    return BadRequest("Invalid IoTDevice name. Please, update IoTDevice with existing name");
                }

                var ioTDevice = _mapper.Map<IoTDevice>(ioTDeviceDTO);

                await _unitOfWork.iot_device.InsertAsync(ioTDevice);
                await _unitOfWork.SaveAsync();

                return CreatedAtRoute("GetIoTDeviceIncludeById", new { id = ioTDevice.Id }, ioTDevice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(InsertIoTDevice)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateIoTDevice(int id, [FromBody] UpdateIoTDeviceDTO ioTDeviceDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateIoTDevice)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ioTDevice = await _unitOfWork.iot_device.GetAsync(x => x.Id == id);

                if (ioTDevice == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateIoTDevice)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(ioTDeviceDTO, ioTDevice);
                _unitOfWork.iot_device.UpdateAsync(ioTDevice);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateIoTDevice)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteIoTDevice(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeleteIoTDevice)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ioTDevice = await _unitOfWork.iot_device.GetAsync(x => x.Id == id);

                if (ioTDevice == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeleteIoTDevice)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.iot_device.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteIoTDevice)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}