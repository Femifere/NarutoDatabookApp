using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Dto;
using NarutoDatabookApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace NarutoDatabookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FanController : Controller
    {
        private readonly IFanInterface _fanInterface;
        private readonly IMapper _mapper;

        public FanController(IFanInterface fanInterface, IMapper mapper)
        {
            _fanInterface = fanInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FanDto>))]
        public IActionResult GetFans()
        {
            var fans = _mapper.Map<List<FanDto>>(_fanInterface.GetFans());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fans);
        }

        [HttpGet("{fanId}")]
        [ProducesResponseType(200, Type = typeof(FanDto))]
        [ProducesResponseType(400)]
        public IActionResult GetFan(int fanId)
        {
            if (!_fanInterface.FanExists(fanId))
                return NotFound();

            var fan = _mapper.Map<FanDto>(_fanInterface.GetFan(fanId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fan);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFan([FromQuery] int rankingId, [FromBody] FanDto fanCreate)
        {
            if (fanCreate == null)
                return BadRequest(ModelState);

            var fan = _fanInterface.GetFans()
                .FirstOrDefault(f => f.Name.Trim().ToUpper() == fanCreate.Name.Trim().ToUpper());

            if (fan != null)
            {
                ModelState.AddModelError("", "Fan already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fanMap = _mapper.Map<Fan>(fanCreate);

            if (!_fanInterface.CreateFan(rankingId, fanMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{fanId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFan(int fanId, [FromQuery] int rankingId, [FromBody] FanDto updatedFan)
        {
            if (updatedFan == null)
                return BadRequest(ModelState);

            if (fanId != updatedFan.FanId)
                return BadRequest(ModelState);

            if (!_fanInterface.FanExists(fanId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var fanMap = _mapper.Map<Fan>(updatedFan);

            if (!_fanInterface.UpdateFan(rankingId, fanMap))
            {
                ModelState.AddModelError("", "Something went wrong updating fan");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{fanId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFan(int fanId)
        {
            if (!_fanInterface.FanExists(fanId))
                return NotFound();

            var fanToDelete = _fanInterface.GetFan(fanId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_fanInterface.DeleteFan(fanToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting fan");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}