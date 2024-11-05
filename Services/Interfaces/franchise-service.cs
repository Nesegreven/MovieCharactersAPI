using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;

        public FranchiseService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .ToListAsync();
        }

        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

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

        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise != null)
            {
                _context.Franchises.Remove(franchise);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchises.AnyAsync(f => f.Id == id);
        }

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

        public async Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);

            return franchise?.Movies ?? new List<Movie>();
        }

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
