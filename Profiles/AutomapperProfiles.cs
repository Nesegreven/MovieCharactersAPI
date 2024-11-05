using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.DTOs.CharacterDTOs;
using MovieCharactersAPI.DTOs.MovieDTOs;
using MovieCharactersAPI.DTOs.FranchiseDTOs;

namespace MovieCharactersAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(dto => dto.MovieIds, opt => opt
                    .MapFrom(character => character.Movies.Select(m => m.Id).ToList()));
            
            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterUpdateDTO, Character>();
        }
    }

    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(dto => dto.CharacterIds, opt => opt
                    .MapFrom(movie => movie.Characters.Select(c => c.Id).ToList()));
            
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();
        }
    }

    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(dto => dto.MovieIds, opt => opt
                    .MapFrom(franchise => franchise.Movies.Select(m => m.Id).ToList()));
            
            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseUpdateDTO, Franchise>();
        }
    }
}
