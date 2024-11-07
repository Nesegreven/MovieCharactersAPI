using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.DTOs.CharacterDTOs;
using MovieCharactersAPI.DTOs.MovieDTOs;
using MovieCharactersAPI.DTOs.FranchiseDTOs;

namespace MovieCharactersAPI.Profiles
{
    /// <summary>
    /// AutoMapper profile for Character entity mappings
    /// </summary>
    public class CharacterProfile : Profile
    {
        /// <summary>
        /// Configures the mappings for Character entities and DTOs
        /// </summary>
        public CharacterProfile()
        {
            // Map from Character entity to CharacterReadDTO
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(dto => dto.MovieIds,
                    opt => opt.MapFrom(character =>
                        character.Movies.Select(m => m.Id).ToList()));

            // Map from CharacterCreateDTO to Character entity
            CreateMap<CharacterCreateDTO, Character>();

            // Map from CharacterUpdateDTO to Character entity
            CreateMap<CharacterUpdateDTO, Character>();
        }
    }

    /// <summary>
    /// AutoMapper profile for Movie entity mappings
    /// </summary>
    public class MovieProfile : Profile
    {
        /// <summary>
        /// Configures the mappings for Movie entities and DTOs
        /// </summary>
        public MovieProfile()
        {
            // Map from Movie entity to MovieReadDTO
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(dto => dto.CharacterIds,
                    opt => opt.MapFrom(movie =>
                        movie.Characters.Select(c => c.Id).ToList()));

            // Map from MovieCreateDTO to Movie entity
            CreateMap<MovieCreateDTO, Movie>();

            // Map from MovieUpdateDTO to Movie entity
            CreateMap<MovieUpdateDTO, Movie>();
        }
    }

    /// <summary>
    /// AutoMapper profile for Franchise entity mappings
    /// </summary>
    public class FranchiseProfile : Profile
    {
        /// <summary>
        /// Configures the mappings for Franchise entities and DTOs
        /// </summary>
        public FranchiseProfile()
        {
            // Map from Franchise entity to FranchiseReadDTO
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(dto => dto.MovieIds,
                    opt => opt.MapFrom(franchise =>
                        franchise.Movies.Select(m => m.Id).ToList()));

            // Map from FranchiseCreateDTO to Franchise entity
            CreateMap<FranchiseCreateDTO, Franchise>();

            // Map from FranchiseUpdateDTO to Franchise entity
            CreateMap<FranchiseUpdateDTO, Franchise>();
        }
    }
}