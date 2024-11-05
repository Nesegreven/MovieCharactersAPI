using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    /// <summary>
    /// Represents a movie franchise in the database
    /// </summary>
    public class Franchise
    {
        /// <summary>
        /// The unique identifier for the franchise
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the franchise
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// A description of the franchise
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Navigation property for the movies in this franchise
        /// </summary>
        public ICollection<Movie> Movies { get; set; }

        // Constructor to initialize the Movies collection
        public Franchise()
        {
            Movies = new HashSet<Movie>();
        }
    }
}
