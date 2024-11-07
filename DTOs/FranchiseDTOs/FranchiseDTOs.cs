using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.FranchiseDTOs
{
    /// <summary>
    /// Data transfer object for reading franchise information
    /// </summary>
    public class FranchiseReadDTO
    {
        /// <summary>
        /// The unique identifier of the franchise
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the franchise
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A description of the franchise (optional)
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// List of movie IDs that belong to this franchise
        /// </summary>
        public List<int> MovieIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// Data transfer object for creating a new franchise
    /// </summary>
    public class FranchiseCreateDTO
    {
        /// <summary>
        /// The name of the franchise. Maximum length is 100 characters.
        /// Example: "Marvel Cinematic Universe"
        /// </summary>
        [Required(ErrorMessage = "Franchise name is required")]
        [MaxLength(100, ErrorMessage = "Franchise name cannot exceed 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the franchise. Maximum length is 1000 characters.
        /// Example: "A series of interconnected superhero films..."
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating an existing franchise
    /// </summary>
    public class FranchiseUpdateDTO
    {
        /// <summary>
        /// The name of the franchise. Maximum length is 100 characters.
        /// Example: "Marvel Cinematic Universe"
        /// </summary>
        [Required(ErrorMessage = "Franchise name is required")]
        [MaxLength(100, ErrorMessage = "Franchise name cannot exceed 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// A description of the franchise. Maximum length is 1000 characters.
        /// Example: "A series of interconnected superhero films..."
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Data transfer object for updating the movies in a franchise
    /// </summary>
    public class FranchiseMoviesUpdateDTO
    {
        /// <summary>
        /// List of movie IDs to be associated with the franchise.
        /// All movies in this list will be set as part of the franchise.
        /// </summary>
        public List<int> MovieIds { get; set; } = new List<int>();
    }
}