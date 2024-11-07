using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    /// <summary>
    /// Service class for managing franchises.
    /// </summary>
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FranchiseService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public FranchiseService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all franchises asynchronously.
        /// </summary>
        /// <returns>A list of franchises.</returns>
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a franchise by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The franchise identifier.</param>
        /// <returns>The franchise with the specified identifier.</returns>
        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        /// <summary>
        /// Adds a new franchise asynchronously.
        /// </summary>
        /// <param name="franchise">The franchise to add.</param>
        /// <returns>The added franchise.</returns>
        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        /// <summary>
        /// Updates an existing franchise asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the franchise to update.</param>
        /// <param name="franchise">The updated franchise data.</param>
        /// <returns>The updated franchise.</returns>
        public async Task<Franchise> UpdateFranchiseAsync(int id, Franchise franchise)
        {
            var existingFranchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (existingFranchise == null)
                return null;

            existingFranchise.Name = franchise.Name;
            existingFranchise.Description = franchise.Description;

            await _context.SaveChangesAsync();
            return existingFranchise;
        }

        /// <summary>
        /// Deletes a franchise by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the franchise to delete.</param>
        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise != null)
            {
                _context.Franchises.Remove(franchise);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if a franchise exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The franchise identifier.</param>
        /// <returns>True if the franchise exists, otherwise false.</returns>
        public async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchises.AnyAsync(f => f.Id == id);
        }

        /// <summary>
        /// Updates the movies associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <param name="movieIds">The list of movie identifiers to associate with the franchise.</param>
        public async Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movieIds)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);

            if (franchise == null)
                throw new KeyNotFoundException($"Franchise with ID {franchiseId} not found");

            var movies = await _context.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToListAsync();

            foreach (var movie in franchise.Movies.ToList())
            {
                movie.FranchiseId = null;
            }

            foreach (var movie in movies)
            {
                movie.FranchiseId = franchiseId;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the movies associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <returns>A list of movies in the franchise.</returns>
        public async Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);

            return franchise?.Movies ?? new List<Movie>();
        }

        /// <summary>
        /// Gets the characters associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <returns>A list of characters in the franchise.</returns>
        public async Task<IEnumerable<Character>> GetCharactersInFranchiseAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .ThenInclude(m => m.Characters)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);

            if (franchise == null)
                return new List<Character>();

            return franchise.Movies
                .SelectMany(m => m.Characters)
                .Distinct();
        }
    }
}
