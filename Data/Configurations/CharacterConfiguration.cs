using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    /// <summary>
    /// Configuration class for the Character entity, implementing Entity Framework's IEntityTypeConfiguration
    /// Defines the database schema and relationships for the Characters table
    /// </summary>
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        /// <summary>
        /// Configures the Character entity type with its properties and relationships
        /// </summary>
        /// <param name="builder">The builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            // Configure primary key
            builder.HasKey(c => c.Id);

            // Configure required full name with maximum length
            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure optional alias with maximum length
            builder.Property(c => c.Alias)
                .HasMaxLength(50);

            // Configure required gender with maximum length
            builder.Property(c => c.Gender)
                .IsRequired()
                .HasMaxLength(20);

            // Configure optional picture URL with maximum length
            builder.Property(c => c.PictureUrl)
                .HasMaxLength(200);

            // Configure many-to-many relationship with Movies
            // This will use the join table defined in MovieConfiguration
            builder.HasMany(c => c.Movies)
                .WithMany(m => m.Characters);
        }
    }
}