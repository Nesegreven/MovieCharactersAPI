using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string MovieTitle { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

        [Required]
        [Range(1888, 2024)]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        [MaxLength(200)]
        public string? TrailerUrl { get; set; }

        // Changed to nullable int
        public int? FranchiseId { get; set; }

        // Navigation properties
        public Franchise? Franchise { get; set; }
        public ICollection<Character> Characters { get; set; }

        public Movie()
        {
            Characters = new HashSet<Character>();
        }
    }
}