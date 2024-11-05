using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    /// <summary>
    /// Represents a character in the movie database
    /// </summary>
    public class Character
    {
        /// <summary>
        /// The unique identifier for the character
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The full name of the character
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        /// <summary>
        /// The alias/nickname of the character (optional)
        /// </summary>
        [MaxLength(50)]
        public string? Alias { get; set; }

        /// <summary>
        /// The gender of the character
        /// </summary>
        [MaxLength(20)]
        public string Gender { get; set; }

        /// <summary>
        /// URL to the character's picture
        /// </summary>
        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// Navigation property for the movies this character appears in
        /// </summary>
        public ICollection<Movie> Movies { get; set; }

        // Constructor to initialize the Movies collection
        public Character()
        {
            Movies = new HashSet<Movie>();
        }
    }
}
