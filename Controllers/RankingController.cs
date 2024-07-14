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
    public class RankingController : Controller
    {
        private readonly IRankingInterface _rankingInterface;
        private readonly IMapper _mapper;

        public RankingController(IRankingInterface rankingInterface, IMapper mapper)
        {
            _rankingInterface = rankingInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RankingDto>))]
        public IActionResult GetRankings()
        {
            var rankings = _mapper.Map<List<RankingDto>>(_rankingInterface.GetRankings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rankings);
        }

        [HttpGet("{rankingId}")]
        [ProducesResponseType(200, Type = typeof(RankingDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRanking(int rankingId)
        {
            if (!_rankingInterface.RankingExists(rankingId))
                return NotFound();

            var ranking = _mapper.Map<RankingDto>(_rankingInterface.GetRanking(rankingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ranking);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRanking([FromBody] RankingDto rankingCreate)
        {
            if (rankingCreate == null)
                return BadRequest(ModelState);

            var ranking = _rankingInterface.GetRankings()
                .FirstOrDefault(r => r.Name.Trim().ToUpper() == rankingCreate.Name.Trim().ToUpper());

            if (ranking != null)
            {
                ModelState.AddModelError("", "Ranking already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rankingMap = _mapper.Map<Ranking>(rankingCreate);

            if (!_rankingInterface.CreateRanking(rankingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{rankingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRanking(int rankingId, [FromBody] RankingDto updatedRanking)
        {
            if (updatedRanking == null)
                return BadRequest(ModelState);

            if (rankingId != updatedRanking.RankingId)
                return BadRequest(ModelState);

            if (!_rankingInterface.RankingExists(rankingId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var rankingMap = _mapper.Map<Ranking>(updatedRanking);

            if (!_rankingInterface.UpdateRanking(rankingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ranking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{rankingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRanking(int rankingId)
        {
            if (!_rankingInterface.RankingExists(rankingId))
                return NotFound();

            var rankingToDelete = _rankingInterface.GetRanking(rankingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_rankingInterface.DeleteRanking(rankingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ranking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}