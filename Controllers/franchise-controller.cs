using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.DTOs.FranchiseDTOs;
using MovieCharactersAPI.DTOs.MovieDTOs;
using MovieCharactersAPI.DTOs.CharacterDTOs;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Controllers
{
    /// <summary>
    /// API endpoints for managing movie franchises
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the FranchisesController
        /// </summary>
        public FranchisesController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises
        /// </summary>
        /// <returns>A list of all franchises</returns>
        /// <response code="200">Returns the list of franchises</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
            var franchises = await _franchiseService.GetAllFranchisesAsync();
            return Ok(_mapper.Map<IEnumerable<FranchiseReadDTO>>(franchises));
        }

        /// <summary>
        /// Gets a specific franchise by id
        /// </summary>
        /// <param name="id">The id of the franchise to get</param>
        /// <returns>The requested franchise</returns>
        /// <response code="200">Returns the requested franchise</response>
        /// <response code="404">If the franchise is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            var franchise = await _franchiseService.GetFranchiseByIdAsync(id);
            if (franchise == null)
            {
                throw new NotFoundException(nameof(Franchise), id);
            }

            return Ok(_mapper.Map<FranchiseReadDTO>(franchise));
        }

        /// <summary>
        /// Creates a new franchise
        /// </summary>
        /// <param name="franchiseDto">The franchise to create</param>
        /// <returns>The created franchise</returns>
        /// <response code="201">Returns the created franchise</response>
        /// <response code="400">If the franchise data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FranchiseReadDTO>> CreateFranchise(FranchiseCreateDTO franchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(franchiseDto);
            var createdFranchise = await _franchiseService.AddFranchiseAsync(franchise);

            return CreatedAtAction(
                nameof(GetFranchise),
                new { id = createdFranchise.Id },
                _mapper.Map<FranchiseReadDTO>(createdFranchise));
        }

        /// <summary>
        /// Updates a specific franchise
        /// </summary>
        /// <param name="id">The id of the franchise to update</param>
        /// <param name="franchiseDto">The updated franchise data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="404">If the franchise is not found</response>
        /// <response code="400">If the franchise data is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFranchise(int id, FranchiseUpdateDTO franchiseDto)
        {
            if (!await _franchiseService.FranchiseExistsAsync(id))
            {
                throw new NotFoundException(nameof(Franchise), id);
            }

            var franchise = _mapper.Map<Franchise>(franchiseDto);
            await _franchiseService.UpdateFranchiseAsync(id, franchise);

            return NoContent();
        }

        /// <summary>
        /// Updates the movies in a franchise
        /// </summary>
        /// <param name="id">The id of the franchise to update</param>
        /// <param name="movieIds">The list of movie ids to associate with the franchise</param>
        /// <returns>No content</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="404">If the franchise is not found</response>
        [HttpPut("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFranchiseMovies(int id, FranchiseMoviesUpdateDTO movieIds)
        {
            await _franchiseService.UpdateFranchiseMoviesAsync(id, movieIds.MovieIds);
            return NoContent();
        }

        /// <summary>
        /// Gets all movies in a specific franchise
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <returns>List of movies in the franchise</returns>
        /// <response code="200">Returns the list of movies</response>
        /// <response code="404">If the franchise is not found</response>
        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetFranchiseMovies(int id)
        {
            if (!await _franchiseService.FranchiseExistsAsync(id))
            {
                throw new NotFoundException(nameof(Franchise), id);
            }

            var movies = await _franchiseService.GetMoviesInFranchiseAsync(id);
            return Ok(_mapper.Map<IEnumerable<MovieReadDTO>>(movies));
        }

        /// <summary>
        /// Gets all characters in a specific franchise
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <returns>List of characters in the franchise</returns>
        /// <response code="200">Returns the list of characters</response>
        /// <response code="404">If the franchise is not found</response>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetFranchiseCharacters(int id)
        {
            if (!await _franchiseService.FranchiseExistsAsync(id))
            {
                throw new NotFoundException(nameof(Franchise), id);
            }

            var characters = await _franchiseService.GetCharactersInFranchiseAsync(id);
            return Ok(_mapper.Map<IEnumerable<CharacterReadDTO>>(characters));
        }

        /// <summary>
        /// Deletes a specific franchise
        /// </summary>
        /// <param name="id">The id of the franchise to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="404">If the franchise is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!await _franchiseService.FranchiseExistsAsync(id))
            {
                throw new NotFoundException(nameof(Franchise), id);
            }

            await _franchiseService.DeleteFranchiseAsync(id);

            return NoContent();
        }
    }
}
