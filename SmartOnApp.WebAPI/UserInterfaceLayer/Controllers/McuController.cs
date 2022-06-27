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
            if (!ModelState.IsValid || mcuDTO.McuMacAddress.Count() >= 1)
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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMcu(int id, [FromBody] UpdateMcuDTO mcuDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateMcu)}");
                return BadRequest(ModelState);
            }

            try
            {
                var mcu = await _unitOfWork.mcu.GetAsync(x => x.Id == id);

                if (mcu == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateMcu)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(mcuDTO, mcu);
                _unitOfWork.mcu.UpdateAsync(mcu);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMcu)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMcu(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeleteMcu)}");
                return BadRequest(ModelState);
            }

            try
            {
                var mcu = await _unitOfWork.mcu.GetAsync(x => x.Id == id);

                if (mcu == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeleteMcu)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.mcu.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteMcu)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}
