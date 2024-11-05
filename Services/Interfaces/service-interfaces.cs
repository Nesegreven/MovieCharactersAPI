using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(int id);
        Task<Character> AddCharacterAsync(Character character);
        Task<Character> UpdateCharacterAsync(int id, Character character);
        Task DeleteCharacterAsync(int id);
        Task<bool> CharacterExistsAsync(int id);
    }

    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(int id, Movie movie);
        Task DeleteMovieAsync(int id);
        Task<bool> MovieExistsAsync(int id);
        Task UpdateMovieCharactersAsync(int movieId, List<int> characterIds);
        Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId);
    }

    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        Task<Franchise> GetFranchiseByIdAsync(int id);
        Task<Franchise> AddFranchiseAsync(Franchise franchise);
        Task<Franchise> UpdateFranchiseAsync(int id, Franchise franchise);
        Task DeleteFranchiseAsync(int id);
        Task<bool> FranchiseExistsAsync(int id);
        Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movieIds);
        Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int franchiseId);
        Task<IEnumerable<Character>> GetCharactersInFranchiseAsync(int franchiseId);
    }
}
