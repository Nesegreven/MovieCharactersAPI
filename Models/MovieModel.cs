using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    /// <summary>
    /// Represents a movie in the database
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The unique identifier for the movie
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the movie
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string MovieTitle { get; set; }

        /// <summary>
        /// The genre(s) of the movie (comma-separated)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

        /// <summary>
        /// The year the movie was released (1888 or later)
        /// </summary>
        [Required]
        [Range(1888, 2024)]
        public int ReleaseYear { get; set; }

        /// <summary>
        /// The director of the movie
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        /// <summary>
        /// URL to the movie poster image
        /// </summary>
        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// URL to the movie trailer
        /// </summary>
        [MaxLength(200)]
        public string? TrailerUrl { get; set; }

        /// <summary>
        /// The ID of the franchise this movie belongs to (optional)
        /// </summary>
        public int? FranchiseId { get; set; }

        /// <summary>
        /// Navigation property for the franchise this movie belongs to
        /// </summary>
        public Franchise? Franchise { get; set; }

        /// <summary>
        /// Collection of characters appearing in this movie
        /// </summary>
        public ICollection<Character> Characters { get; set; }

        /// <summary>
        /// Initializes a new instance of the Movie class
        /// </summary>
        public Movie()
        {
            Characters = new HashSet<Character>();
        }
    }
}