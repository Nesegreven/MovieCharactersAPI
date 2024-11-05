using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Data.Configurations
{
    public class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
    {
        public void Configure(EntityTypeBuilder<Franchise> builder)
        {
            // Primary Key
            builder.HasKey(f => f.Id);

            // Properties
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Description)
                .HasMaxLength(1000);

            // Relationships
            builder.HasMany(f => f.Movies)
                .WithOne(m => m.Franchise)
                .HasForeignKey(m => m.FranchiseId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
