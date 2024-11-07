using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    /// <summary>
    /// Configuration class for the Movie entity, implementing Entity Framework's IEntityTypeConfiguration
    /// Defines the database schema and relationships for the Movies table
    /// </summary>
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        /// <summary>
        /// Configures the Movie entity type with its properties and relationships
        /// </summary>
        /// <param name="builder">The builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Configure primary key
            builder.HasKey(m => m.Id);

            // Configure required string properties with maximum lengths
            builder.Property(m => m.MovieTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(100);

            // Configure required numeric properties
            builder.Property(m => m.ReleaseYear)
                .IsRequired();

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(100);

            // Configure optional URL properties
            builder.Property(m => m.PictureUrl)
                .HasMaxLength(200);

            builder.Property(m => m.TrailerUrl)
                .HasMaxLength(200);

            // Configure relationship with Franchise (One-to-Many)
            builder.HasOne(m => m.Franchise)
                .WithMany(f => f.Movies)
                .HasForeignKey(m => m.FranchiseId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with Characters (Many-to-Many)
            builder.HasMany(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity(j => j.ToTable("MovieCharacter")); // Creates join table named "MovieCharacter"
        }
    }
}