using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    /// <summary>
    /// Service class for managing movies.
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly MovieCharactersDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MovieService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all movies asynchronously.
        /// </summary>
        /// <returns>A list of movies.</returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .Include(m => m.Franchise)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a movie by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The movie identifier.</param>
        /// <returns>The movie with the specified identifier.</returns>
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .Include(m => m.Franchise)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// Adds a new movie asynchronously.
        /// </summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        /// <summary>
        /// Updates an existing movie asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the movie to update.</param>
        /// <param name="movie">The updated movie data.</param>
        /// <returns>The updated movie.</returns>
        public async Task<Movie> UpdateMovieAsync(int id, Movie movie)
        {
            var existingMovie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
                return null;

            existingMovie.MovieTitle = movie.MovieTitle;
            existingMovie.Genre = movie.Genre;
            existingMovie.ReleaseYear = movie.ReleaseYear;
            existingMovie.Director = movie.Director;
            existingMovie.PictureUrl = movie.PictureUrl;
            existingMovie.TrailerUrl = movie.TrailerUrl;
            existingMovie.FranchiseId = movie.FranchiseId;

            await _context.SaveChangesAsync();
            return existingMovie;
        }

        /// <summary>
        /// Deletes a movie by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the movie to delete.</param>
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if a movie exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The movie identifier.</param>
        /// <returns>True if the movie exists, otherwise false.</returns>
        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        /// <summary>
        /// Updates the characters associated with a movie asynchronously.
        /// </summary>
        /// <param name="movieId">The identifier of the movie.</param>
        /// <param name="characterIds">The list of character identifiers to associate with the movie.</param>
        public async Task UpdateMovieCharactersAsync(int movieId, List<int> characterIds)
        {
            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException($"Movie with ID {movieId} not found");

            movie.Characters.Clear();
            var characters = await _context.Characters
                .Where(c => characterIds.Contains(c.Id))
                .ToListAsync();

            foreach (var character in characters)
            {
                movie.Characters.Add(character);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the characters associated with a movie asynchronously.
        /// </summary>
        /// <param name="movieId">The identifier of the movie.</param>
        /// <returns>A list of characters in the movie.</returns>
        public async Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.Characters ?? new List<Character>();
        }
    }
}
