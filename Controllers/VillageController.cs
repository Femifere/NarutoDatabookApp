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
    public class VillageController : Controller
    {
        private readonly IVillageInterface _villageInterface;
        private readonly IMapper _mapper;

        public VillageController(IVillageInterface villageInterface, IMapper mapper)
        {
            _villageInterface = villageInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VillageDto>))]
        public IActionResult GetVillages()
        {
            var villages = _mapper.Map<List<VillageDto>>(_villageInterface.GetVillages());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(villages);
        }

        [HttpGet("{villageId}")]
        [ProducesResponseType(200, Type = typeof(VillageDto))]
        [ProducesResponseType(400)]
        public IActionResult GetVillage(int villageId)
        {
            if (!_villageInterface.VillageExists(villageId))
                return NotFound();

            var village = _mapper.Map<VillageDto>(_villageInterface.GetVillage(villageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(village);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVillage([FromBody] VillageDto villageCreate)
        {
            if (villageCreate == null)
                return BadRequest(ModelState);

            var village = _villageInterface.GetVillages()
                .FirstOrDefault(v => v.Name.Trim().ToUpper() == villageCreate.Name.Trim().ToUpper());

            if (village != null)
            {
                ModelState.AddModelError("", "Village already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var villageMap = _mapper.Map<Village>(villageCreate);

            if (!_villageInterface.CreateVillage(villageMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{villageId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVillage(int villageId, [FromBody] VillageDto updatedVillage)
        {
            if (updatedVillage == null)
                return BadRequest(ModelState);

            if (villageId != updatedVillage.VillageId)
                return BadRequest(ModelState);

            if (!_villageInterface.VillageExists(villageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var villageMap = _mapper.Map<Village>(updatedVillage);

            if (!_villageInterface.UpdateVillage(villageMap))
            {
                ModelState.AddModelError("", "Something went wrong updating village");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{villageId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVillage(int villageId)
        {
            if (!_villageInterface.VillageExists(villageId))
                return NotFound();

            var villageToDelete = _villageInterface.GetVillage(villageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_villageInterface.DeleteVillage(villageToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting village");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}


