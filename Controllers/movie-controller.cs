using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.DTOs.MovieDTOs;
using MovieCharactersAPI.DTOs.CharacterDTOs;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Controllers
{
    /// <summary>
    /// API endpoints for managing movies
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the MoviesController
        /// </summary>
        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <returns>A list of all movies</returns>
        /// <response code="200">Returns the list of movies</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(_mapper.Map<IEnumerable<MovieReadDTO>>(movies));
        }

        /// <summary>
        /// Gets a specific movie by id
        /// </summary>
        /// <param name="id">The id of the movie to get</param>
        /// <returns>The requested movie</returns>
        /// <response code="200">Returns the requested movie</response>
        /// <response code="404">If the movie is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new NotFoundException(nameof(Movie), id);
            }

            return Ok(_mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="movieDto">The movie to create</param>
        /// <returns>The created movie</returns>
        /// <response code="201">Returns the created movie</response>
        /// <response code="400">If the movie data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieReadDTO>> CreateMovie(MovieCreateDTO movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            var createdMovie = await _movieService.AddMovieAsync(movie);

            return CreatedAtAction(
                nameof(GetMovie),
                new { id = createdMovie.Id },
                _mapper.Map<MovieReadDTO>(createdMovie));
        }

        /// <summary>
        /// Updates a specific movie
        /// </summary>
        /// <param name="id">The id of the movie to update</param>
        /// <param name="movieDto">The updated movie data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="404">If the movie is not found</response>
        /// <response code="400">If the movie data is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDTO movieDto)
        {
            if (!await _movieService.MovieExistsAsync(id))
            {
                throw new NotFoundException(nameof(Movie), id);
            }

            var movie = _mapper.Map<Movie>(movieDto);
            await _movieService.UpdateMovieAsync(id, movie);

            return NoContent();
        }

        /// <summary>
        /// Updates the characters in a movie
        /// </summary>
        /// <param name="id">The id of the movie to update</param>
        /// <param name="characterIds">The list of character ids to associate with the movie</param>
        /// <returns>No content</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="404">If the movie is not found</response>
        [HttpPut("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovieCharacters(int id, MovieCharactersUpdateDTO characterIds)
        {
            await _movieService.UpdateMovieCharactersAsync(id, characterIds.CharacterIds);
            return NoContent();
        }

        /// <summary>
        /// Gets all characters in a specific movie
        /// </summary>
        /// <param name="id">The id of the movie</param>
        /// <returns>List of characters in the movie</returns>
        /// <response code="200">Returns the list of characters</response>
        /// <response code="404">If the movie is not found</response>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetMovieCharacters(int id)
        {
            if (!await _movieService.MovieExistsAsync(id))
            {
                throw new NotFoundException(nameof(Movie), id);
            }

            var characters = await _movieService.GetCharactersInMovieAsync(id);
            return Ok(_mapper.Map<IEnumerable<CharacterReadDTO>>(characters));
        }

        /// <summary>
        /// Deletes a specific movie
        /// </summary>
        /// <param name="id">The id of the movie to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="404">If the movie is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!await _movieService.MovieExistsAsync(id))
            {
                throw new NotFoundException(nameof(Movie), id);
            }

            await _movieService.DeleteMovieAsync(id);

            return NoContent();
        }
    }
}
