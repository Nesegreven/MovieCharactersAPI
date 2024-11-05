using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.MovieDTOs
{
    /// <summary>
    /// DTO for reading movie data
    /// </summary>
    public class MovieReadDTO
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string? PictureUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public int? FranchiseId { get; set; }
        public List<int> CharacterIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// DTO for creating a new movie
    /// </summary>
    public class MovieCreateDTO
    {
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

        public int? FranchiseId { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing movie
    /// </summary>
    public class MovieUpdateDTO
    {
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

        public int? FranchiseId { get; set; }
    }

    /// <summary>
    /// DTO for updating characters in a movie
    /// </summary>
    public class MovieCharactersUpdateDTO
    {
        public List<int> CharacterIds { get; set; } = new List<int>();
    }
}
