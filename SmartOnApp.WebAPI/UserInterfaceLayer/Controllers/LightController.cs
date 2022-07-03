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

        [HttpGet("all", Name = "GetAllLightInclude")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLightInclude()
        {
            try
            {
                var allLight = await _unitOfWork.light.GetAllAsync(include: y => y.Include(x => x.IoTDevice).ThenInclude(z => z.Mcu));
                var result = _mapper.Map<IList<LightDTO>>(allLight);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLightInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetLightIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLightIncludeById(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP GET request in {nameof(GetLightIncludeById)}" +
                    $"{id} is invalid");
                return BadRequest("Invalid id");
            }

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

        [HttpPost()]
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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLight(int id, [FromBody] UpdateLightDTO lightDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateLight)}");
                return BadRequest(ModelState);
            }

            try
            {
                var light = await _unitOfWork.light.GetAsync(x => x.Id == id);

                if (light == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdateLight)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(lightDTO, light);
                _unitOfWork.light.UpdateAsync(light);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateLight)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLight(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeleteLight)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ldr = await _unitOfWork.light.GetAsync(x => x.Id == id);

                if (ldr == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeleteLight)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.light.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteLight)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}