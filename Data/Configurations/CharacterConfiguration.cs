using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Alias)
                .HasMaxLength(50);

            builder.Property(c => c.Gender)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.PictureUrl)
                .HasMaxLength(200);

            // Relationships
            builder.HasMany(c => c.Movies)
                .WithMany(m => m.Characters);
        }
    }
}
