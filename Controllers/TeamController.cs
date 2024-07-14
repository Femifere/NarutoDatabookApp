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
    public class TeamController : Controller
    {
        private readonly ITeamInterface _teamInterface;
        private readonly IMapper _mapper;

        public TeamController(ITeamInterface teamInterface, IMapper mapper)
        {
            _teamInterface = teamInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeamDto>))]
        public IActionResult GetTeams()
        {
            var teams = _mapper.Map<List<TeamDto>>(_teamInterface.GetTeams());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        [ProducesResponseType(200, Type = typeof(TeamDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTeam(int teamId)
        {
            if (!_teamInterface.TeamExists(teamId))
                return NotFound();

            var team = _mapper.Map<TeamDto>(_teamInterface.GetTeam(teamId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(team);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeam([FromQuery] int villageId, [FromBody] TeamDto teamCreate)
        {
            if (teamCreate == null)
                return BadRequest(ModelState);

            var team = _teamInterface.GetTeams()
                .FirstOrDefault(t => t.Name.Trim().ToUpper() == teamCreate.Name.Trim().ToUpper());

            if (team != null)
            {
                ModelState.AddModelError("", "Team already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teamMap = _mapper.Map<Team>(teamCreate);

            if (!_teamInterface.CreateTeam(villageId, teamMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{teamId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeam(int teamId, [FromQuery] int villageId, [FromBody] TeamDto updatedTeam)
        {
            if (updatedTeam == null)
                return BadRequest(ModelState);

            if (teamId != updatedTeam.TeamId)
                return BadRequest(ModelState);

            if (!_teamInterface.TeamExists(teamId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var teamMap = _mapper.Map<Team>(updatedTeam);

            if (!_teamInterface.UpdateTeam(villageId, teamMap))
            {
                ModelState.AddModelError("", "Something went wrong updating team");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{teamId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeam(int teamId)
        {
            if (!_teamInterface.TeamExists(teamId))
                return NotFound();

            var teamToDelete = _teamInterface.GetTeam(teamId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teamInterface.DeleteTeam(teamToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting team");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}


