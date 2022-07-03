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
    public class PirController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPirRepository _pirRepository;
        private readonly ILogger<PirController> _logger;
        private readonly IMapper _mapper;

        public PirController(IUnitOfWork unitOfWork,
            IPirRepository pirRepository,
            ILogger<PirController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _pirRepository = pirRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("all", Name = "GetAllPirIncludeByMacAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPirInclude()
        {
            try
            {
                var allPir = await _unitOfWork.pir.GetAllAsync(include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<IList<PirDTO>>(allPir);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllPirInclude)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetPirIncludeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPirIncludeById(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP GET request in {nameof(GetPirIncludeById)}" +
                    $"{id} is invalid");
                return BadRequest("Invalid id");
            }

            try
            {
                var pir = await _unitOfWork.pir.GetAsync(x => x.Id == id, include: y => y.Include(x => x.IoTDevice));
                var result = _mapper.Map<PirDTO>(pir);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetPirIncludeById)}");
                return StatusCode(500, "Internal server errror. Please, try again later.");
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertPir([FromBody] CreatePirDTO pirDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(InsertPir)}");
                return BadRequest(ModelState);
            }

            try
            {
                var pir = _mapper.Map<Pir>(pirDTO);

                await _unitOfWork.pir.InsertAsync(pir);
                await _unitOfWork.SaveAsync();
                return CreatedAtRoute("GetPirIncludeById", new { id = pir.Id }, pir);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(InsertPir)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePir(int id, [FromBody] UpdatePirDTO pirDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdatePir)}");
                return BadRequest(ModelState);
            }

            try
            {
                var pir = await _unitOfWork.pir.GetAsync(x => x.Id == id);

                if (pir == null)
                {
                    _logger.LogError($"Invalid HTTP PUT request in {nameof(UpdatePir)}");
                    return BadRequest("Submitted invalid data");
                }

                _mapper.Map(pirDTO, pir);
                _unitOfWork.pir.UpdateAsync(pir);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdatePir)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePir(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid HTTP PUT request in {nameof(DeletePir)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ldr = await _unitOfWork.pir.GetAsync(x => x.Id == id);

                if (ldr == null)
                {
                    _logger.LogError($"Invalid HTTP DELETE request in {nameof(DeletePir)}");
                    return BadRequest("Submitted invalid data");
                }

                await _unitOfWork.pir.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeletePir)}");
                return StatusCode(500, "Internal server error. Please, try again later.");
            }
        }
    }
}