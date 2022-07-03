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
    public class LdrController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILdrRepository _ldrRepository;
        private readonly ILogger<LdrController> _logger;
        private readonly IMapper _mapper;

        public LdrController(IUnitOfWork unitOfWork,
            ILdrRepository ldrRepository,
            ILogger<LdrController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ldrRepository = ldrRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("all/{macAddress}", Name = "GetAllLdrIncludeByMacAddress")]
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
                var allLdrIncludeByMacAddress = await _unitOfWork.ldr.GetAllAsync(include: y => y.Include(x => x.IoTDevice).ThenInclude(z => z.Mcu).ThenInclude(t => t.McuMacAddress));
                var result = _mapper.Map<IList<LdrDTO>>(allLdrIncludeByMacAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLdrIncludeByMacAddress)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetLdrIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLdrIncludeById(int id)
        {
            try
            {
                var ldr = await _unitOfWork.ldr.GetAsync(x => x.Id == id, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<LdrDTO>(ldr);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLdrIncludeById)}");
                return StatusCode(500, "Internal server errror. Please, try again later.");
            }
        }

        // TODO also update IoTDevice UpdatedAt ??
        [HttpPost("ldr")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertLdr([FromBody] CreateLdrDTO ldrDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(InsertLdr)}");
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
                _logger.LogError(ex, $"Something went wrong in the {nameof(InsertLdr)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLdr(int id, [FromBody] UpdateLdrDTO ldrDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateLdr)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ldr = await _unitOfWork.ldr.GetAsync(x => x.Id == id);

                if (ldr == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateLdr)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(ldrDTO, ldr);
                _unitOfWork.ldr.UpdateAsync(ldr);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateLdr)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLdr(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeleteLdr)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ldr = await _unitOfWork.ldr.GetAsync(x => x.Id == id);

                if (ldr == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeleteLdr)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.ldr.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteLdr)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}

