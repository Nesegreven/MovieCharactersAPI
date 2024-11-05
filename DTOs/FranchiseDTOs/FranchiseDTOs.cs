using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.FranchiseDTOs
{
    /// <summary>
    /// DTO for reading franchise data
    /// </summary>
    public class FranchiseReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<int> MovieIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// DTO for creating a new franchise
    /// </summary>
    public class FranchiseCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing franchise
    /// </summary>
    public class FranchiseUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO for updating movies in a franchise
    /// </summary>
    public class FranchiseMoviesUpdateDTO
    {
        public List<int> MovieIds { get; set; } = new List<int>();
    }
}
