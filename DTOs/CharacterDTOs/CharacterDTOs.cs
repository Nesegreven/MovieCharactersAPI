using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.CharacterDTOs
{
    /// <summary>
    /// Data transfer object for reading character information
    /// </summary>
    public class CharacterReadDTO
    {
        /// <summary>
        /// The unique identifier of the character
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The full name of the character
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The alias or alternate name of the character (if any)
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// The gender of the character
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// URL to the character's picture
        /// </summary>
        public string? PictureUrl { get; set; }

        /// <summary>
        /// List of movie IDs that this character appears in
        /// </summary>
        public List<int> MovieIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// Data transfer object for creating a new character
    /// </summary>
    public class CharacterCreateDTO
    {
        /// <summary>
        /// The full name of the character. Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; }

        /// <summary>
        /// The alias or alternate name of the character (optional). Maximum length is 50 characters.
        /// </summary>
        [MaxLength(50, ErrorMessage = "Alias cannot exceed 50 characters")]
        public string? Alias { get; set; }

        /// <summary>
        /// The gender of the character. Maximum length is 20 characters.
        /// </summary>
        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        public string Gender { get; set; }

        /// <summary>
        /// URL to the character's picture (optional). Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Picture URL cannot exceed 200 characters")]
        public string? PictureUrl { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating an existing character
    /// </summary>
    public class CharacterUpdateDTO
    {
        /// <summary>
        /// The full name of the character. Maximum length is 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; }

        /// <summary>
        /// The alias or alternate name of the character (optional). Maximum length is 50 characters.
        /// </summary>
        [MaxLength(50, ErrorMessage = "Alias cannot exceed 50 characters")]
        public string? Alias { get; set; }

        /// <summary>
        /// The gender of the character. Maximum length is 20 characters.
        /// </summary>
        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        public string Gender { get; set; }

        /// <summary>
        /// URL to the character's picture (optional). Maximum length is 200 characters.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Picture URL cannot exceed 200 characters")]
        public string? PictureUrl { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating the movies a character appears in
    /// </summary>
    public class CharacterMoviesUpdateDTO
    {
        /// <summary>
        /// List of movie IDs that the character should be associated with
        /// </summary>
        public List<int> MovieIds { get; set; } = new List<int>();
    }
}