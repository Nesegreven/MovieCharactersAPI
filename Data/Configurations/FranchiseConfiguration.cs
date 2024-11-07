using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    /// <summary>
    /// Configuration class for the Franchise entity, implementing Entity Framework's IEntityTypeConfiguration
    /// Defines the database schema and relationships for the Franchises table
    /// </summary>
    public class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
    {
        /// <summary>
        /// Configures the Franchise entity type with its properties and relationships
        /// </summary>
        /// <param name="builder">The builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<Franchise> builder)
        {
            // Configure primary key
            builder.HasKey(f => f.Id);

            // Configure required string property with maximum length
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure optional description with maximum length
            builder.Property(f => f.Description)
                .HasMaxLength(1000);

            // Configure one-to-many relationship with Movies
            // When a franchise is deleted, set the FranchiseId to null in related movies
            builder.HasMany(f => f.Movies)
                .WithOne(m => m.Franchise)
                .HasForeignKey(m => m.FranchiseId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}