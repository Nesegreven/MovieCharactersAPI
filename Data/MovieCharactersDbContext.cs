using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Data.Configurations;

namespace MovieCharactersAPI.Data
{
    public class MovieCharactersDbContext : DbContext
    {
        public MovieCharactersDbContext(DbContextOptions<MovieCharactersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new CharacterConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new FranchiseConfiguration());
        }
    }
}