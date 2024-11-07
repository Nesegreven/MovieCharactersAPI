using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Data.Configurations;

namespace MovieCharactersAPI.Data
{
    /// <summary>
    /// Database context for the Movie Characters API, handling the database connection and model configurations
    /// </summary>
    public class MovieCharactersDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the MovieCharactersDbContext with the specified options
        /// </summary>
        /// <param name="options">The options to be used by the DbContext</param>
        public MovieCharactersDbContext(DbContextOptions<MovieCharactersDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Characters DbSet
        /// Represents the Characters table in the database
        /// </summary>
        public DbSet<Character> Characters { get; set; }

        /// <summary>
        /// Gets or sets the Movies DbSet
        /// Represents the Movies table in the database
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Gets or sets the Franchises DbSet
        /// Represents the Franchises table in the database
        /// </summary>
        public DbSet<Franchise> Franchises { get; set; }

        /// <summary>
        /// Configures the model relationships and constraints when the model is being created
        /// Applies configurations from separate configuration classes for each entity
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations from separate configuration classes
            modelBuilder.ApplyConfiguration(new CharacterConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new FranchiseConfiguration());
        }
    }
}