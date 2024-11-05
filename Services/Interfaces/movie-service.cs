using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieCharactersDbContext _context;

        public MovieService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .Include(m => m.Franchise)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .Include(m => m.Franchise)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

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

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

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

        public async Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.Characters ?? new List<Character>();
        }
    }
}
