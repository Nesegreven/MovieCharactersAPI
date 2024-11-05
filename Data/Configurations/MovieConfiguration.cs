using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);

            // Properties
            builder.Property(m => m.MovieTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.ReleaseYear)
                .IsRequired();

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.PictureUrl)
                .HasMaxLength(200);

            builder.Property(m => m.TrailerUrl)
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(m => m.Franchise)
                .WithMany(f => f.Movies)
                .HasForeignKey(m => m.FranchiseId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity(j => j.ToTable("MovieCharacter")); // Specify the name of the join table
        }
    }
}
