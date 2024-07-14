using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarutoDatabookApp.Dto;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace NarutoDatabookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : Controller
    {
        private readonly ICharacterInterface _characterInterface;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterInterface characterInterface, IMapper mapper)
        {
            _characterInterface = characterInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CharacterDto>))]
        public IActionResult GetCharacters()
        {
            var characters = _mapper.Map<List<CharacterDto>>(_characterInterface.GetCharacters());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(characters);
        }

        [HttpGet("{characterId}")]
        [ProducesResponseType(200, Type = typeof(CharacterDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacter(int characterId)
        {
            if (!_characterInterface.CharacterExists(characterId))
                return NotFound();

            var character = _mapper.Map<CharacterDto>(_characterInterface.GetCharacter(characterId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(character);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCharacter([FromQuery] int teamId, [FromBody] CharacterDto characterCreate)
        {
            if (characterCreate == null)
                return BadRequest(ModelState);

            var character = _characterInterface.GetCharacters()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == characterCreate.Name.Trim().ToUpper());

            if (character != null)
            {
                ModelState.AddModelError("", "Character already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var characterMap = _mapper.Map<Character>(characterCreate);

            if (!_characterInterface.CreateCharacter(teamId, characterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{characterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCharacter(int characterId, [FromQuery] int teamId, [FromBody] CharacterDto updatedCharacter)
        {
            if (updatedCharacter == null)
                return BadRequest(ModelState);

            if (characterId != updatedCharacter.CharacterId)
                return BadRequest(ModelState);

            if (!_characterInterface.CharacterExists(characterId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var characterMap = _mapper.Map<Character>(updatedCharacter);

            if (!_characterInterface.UpdateCharacter(teamId, characterMap))
            {
                ModelState.AddModelError("", "Something went wrong updating character");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{characterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCharacter(int characterId)
        {
            if (!_characterInterface.CharacterExists(characterId))
                return NotFound();

            var characterToDelete = _characterInterface.GetCharacter(characterId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_characterInterface.DeleteCharacter(characterToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting character");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
