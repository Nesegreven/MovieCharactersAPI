using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.DTOs.CharacterDTOs
{
    /// <summary>
    /// DTO for reading character data
    /// </summary>
    public class CharacterReadDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }
        public string? PictureUrl { get; set; }
        public List<int> MovieIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// DTO for creating a new character
    /// </summary>
    public class CharacterCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string? Alias { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(200)]
        public string? PictureUrl { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing character
    /// </summary>
    public class CharacterUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string? Alias { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(200)]
        public string? PictureUrl { get; set; }
    }
}
