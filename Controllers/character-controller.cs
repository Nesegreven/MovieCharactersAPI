using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.DTOs.CharacterDTOs;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Controllers
{
    /// <summary>
    /// API endpoints for managing movie characters
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the CharactersController
        /// </summary>
        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns>A list of all characters</returns>
        /// <response code="200">Returns the list of characters</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            return Ok(_mapper.Map<IEnumerable<CharacterReadDTO>>(characters));
        }

        /// <summary>
        /// Gets a specific character by id
        /// </summary>
        /// <param name="id">The id of the character to get</param>
        /// <returns>The requested character</returns>
        /// <response code="200">Returns the requested character</response>
        /// <response code="404">If the character is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacterByIdAsync(id);
            if (character == null)
            {
                throw new NotFoundException(nameof(Character), id);
            }

            return Ok(_mapper.Map<CharacterReadDTO>(character));
        }

        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="characterDto">The character to create</param>
        /// <returns>The created character</returns>
        /// <response code="201">Returns the created character</response>
        /// <response code="400">If the character data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterReadDTO>> CreateCharacter(CharacterCreateDTO characterDto)
        {
            var character = _mapper.Map<Character>(characterDto);
            var createdCharacter = await _characterService.AddCharacterAsync(character);

            return CreatedAtAction(
                nameof(GetCharacter),
                new { id = createdCharacter.Id },
                _mapper.Map<CharacterReadDTO>(createdCharacter));
        }

        /// <summary>
        /// Updates a specific character
        /// </summary>
        /// <param name="id">The id of the character to update</param>
        /// <param name="characterDto">The updated character data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="404">If the character is not found</response>
        /// <response code="400">If the character data is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCharacter(int id, CharacterUpdateDTO characterDto)
        {
            if (!await _characterService.CharacterExistsAsync(id))
            {
                throw new NotFoundException(nameof(Character), id);
            }

            var character = _mapper.Map<Character>(characterDto);
            await _characterService.UpdateCharacterAsync(id, character);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific character
        /// </summary>
        /// <param name="id">The id of the character to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="404">If the character is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!await _characterService.CharacterExistsAsync(id))
            {
                throw new NotFoundException(nameof(Character), id);
            }

            await _characterService.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}
