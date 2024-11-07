using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.MovieDTOs
{
    /// <summary>
    /// Data transfer object for reading movie information
    /// </summary>
    public class MovieReadDTO
    {
        /// <summary>
        /// The unique identifier of the movie
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the movie
        /// </summary>
        public string MovieTitle { get; set; }

        /// <summary>
        /// The genre(s) of the movie (comma-separated)
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// The year the movie was released
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// The director of the movie
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// URL to the movie poster image
        /// </summary>
        public string? PictureUrl { get; set; }

        /// <summary>
        /// URL to the movie trailer
        /// </summary>
        public string? TrailerUrl { get; set; }

        /// <summary>
        /// The ID of the franchise this movie belongs to
        /// </summary>
        public int? FranchiseId { get; set; }

        /// <summary>
        /// List of character IDs that appear in this movie
        /// </summary>
        public List<int> CharacterIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// Data transfer object for creating a new movie
    /// </summary>
    public class MovieCreateDTO
    {
        /// <summary>
        /// The title of the movie. Maximum length is 200 characters.
        /// </summary>
        [Required(ErrorMessage = "Movie title is required")]
        [MaxLength(200, ErrorMessage = "Movie title cannot exceed 200 characters")]
        public string MovieTitle { get; set; }

        /// <summary>
        /// The genre(s) of the movie (comma-separated). Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(100, ErrorMessage = "Genre cannot exceed 100 characters")]
        public string Genre { get; set; }

        /// <summary>
        /// The year the movie was released. Must be 1888 or later.
        /// </summary>
        [Required(ErrorMessage = "Release year is required")]
        [Range(1888, 2024, ErrorMessage = "Release year must be between 1888 and 2024")]
        public int ReleaseYear { get; set; }

        /// <summary>
        /// The director of the movie. Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Director is required")]
        [MaxLength(100, ErrorMessage = "Director name cannot exceed 100 characters")]
        public string Director { get; set; }

        /// <summary>
        /// URL to the movie poster image. Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Picture URL cannot exceed 200 characters")]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// URL to the movie trailer. Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Trailer URL cannot exceed 200 characters")]
        public string? TrailerUrl { get; set; }

        /// <summary>
        /// The ID of the franchise this movie belongs to (optional)
        /// </summary>
        public int? FranchiseId { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating an existing movie
    /// </summary>
    public class MovieUpdateDTO
    {
        /// <summary>
        /// The title of the movie. Maximum length is 200 characters.
        /// </summary>
        [Required(ErrorMessage = "Movie title is required")]
        [MaxLength(200, ErrorMessage = "Movie title cannot exceed 200 characters")]
        public string MovieTitle { get; set; }

        /// <summary>
        /// The genre(s) of the movie (comma-separated). Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(100, ErrorMessage = "Genre cannot exceed 100 characters")]
        public string Genre { get; set; }

        /// <summary>
        /// The year the movie was released. Must be 1888 or later.
        /// </summary>
        [Required(ErrorMessage = "Release year is required")]
        [Range(1888, 2024, ErrorMessage = "Release year must be between 1888 and 2024")]
        public int ReleaseYear { get; set; }

        /// <summary>
        /// The director of the movie. Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Director is required")]
        [MaxLength(100, ErrorMessage = "Director name cannot exceed 100 characters")]
        public string Director { get; set; }

        /// <summary>
        /// URL to the movie poster image. Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Picture URL cannot exceed 200 characters")]
        public string? PictureUrl { get; set; }

        /// <summary>
        /// URL to the movie trailer. Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Trailer URL cannot exceed 200 characters")]
        public string? TrailerUrl { get; set; }

        /// <summary>
        /// The ID of the franchise this movie belongs to (optional)
        /// </summary>
        public int? FranchiseId { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating the characters in a movie
    /// </summary>
    public class MovieCharactersUpdateDTO
    {
        /// <summary>
        /// List of character IDs to be associated with the movie
        /// </summary>
        public List<int> CharacterIds { get; set; } = new List<int>();
    }
}