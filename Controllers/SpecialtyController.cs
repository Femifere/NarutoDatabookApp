using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using NarutoDatabookApp.Dto;
using System.Collections.Generic;
using System.Linq;

namespace NarutoDatabookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyInterface _specialtyInterface;
        private readonly IMapper _mapper;

        public SpecialtyController(ISpecialtyInterface specialtyInterface, IMapper mapper)
        {
            _specialtyInterface = specialtyInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SpecialtyDto>))]
        public IActionResult GetSpecialties()
        {
            var specialties = _mapper.Map<List<SpecialtyDto>>(_specialtyInterface.GetSpecialties());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(specialties);
        }

        [HttpGet("{specialtyId}")]
        [ProducesResponseType(200, Type = typeof(SpecialtyDto))]
        [ProducesResponseType(400)]
        public IActionResult GetSpecialty(int specialtyId)
        {
            if (!_specialtyInterface.SpecialtyExists(specialtyId))
                return NotFound();

            var specialty = _mapper.Map<SpecialtyDto>(_specialtyInterface.GetSpecialty(specialtyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(specialty);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSpecialty([FromBody] SpecialtyDto specialtyCreate)
        {
            if (specialtyCreate == null)
                return BadRequest(ModelState);

            var specialty = _specialtyInterface.GetSpecialties()
                .FirstOrDefault(s => s.Name.Trim().ToUpper() == specialtyCreate.Name.Trim().ToUpper());

            if (specialty != null)
            {
                ModelState.AddModelError("", "Specialty already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var specialtyMap = _mapper.Map<Specialty>(specialtyCreate);

            if (!_specialtyInterface.CreateSpecialty(specialtyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{specialtyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSpecialty(int specialtyId, [FromBody] SpecialtyDto updatedSpecialty)
        {
            if (updatedSpecialty == null)
                return BadRequest(ModelState);

            if (specialtyId != updatedSpecialty.SpecialtyId)
                return BadRequest(ModelState);

            if (!_specialtyInterface.SpecialtyExists(specialtyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var specialtyMap = _mapper.Map<Specialty>(updatedSpecialty);

            if (!_specialtyInterface.UpdateSpecialty(specialtyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating specialty");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{specialtyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSpecialty(int specialtyId)
        {
            if (!_specialtyInterface.SpecialtyExists(specialtyId))
                return NotFound();

            var specialtyToDelete = _specialtyInterface.GetSpecialty(specialtyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_specialtyInterface.DeleteSpecialty(specialtyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting specialty");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

