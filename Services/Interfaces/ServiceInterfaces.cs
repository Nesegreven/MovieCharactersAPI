using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for character service.
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// Gets all characters asynchronously.
        /// </summary>
        /// <returns>A list of characters.</returns>
        Task<IEnumerable<Character>> GetAllCharactersAsync();

        /// <summary>
        /// Gets a character by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The character identifier.</param>
        /// <returns>The character with the specified identifier.</returns>
        Task<Character> GetCharacterByIdAsync(int id);

        /// <summary>
        /// Adds a new character asynchronously.
        /// </summary>
        /// <param name="character">The character to add.</param>
        /// <returns>The added character.</returns>
        Task<Character> AddCharacterAsync(Character character);

        /// <summary>
        /// Updates an existing character asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the character to update.</param>
        /// <param name="character">The updated character data.</param>
        /// <returns>The updated character.</returns>
        Task<Character> UpdateCharacterAsync(int id, Character character);

        /// <summary>
        /// Deletes a character by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the character to delete.</param>
        Task DeleteCharacterAsync(int id);

        /// <summary>
        /// Checks if a character exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The character identifier.</param>
        /// <returns>True if the character exists, otherwise false.</returns>
        Task<bool> CharacterExistsAsync(int id);
    }

    /// <summary>
    /// Interface for movie service.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Gets all movies asynchronously.
        /// </summary>
        /// <returns>A list of movies.</returns>
        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        /// <summary>
        /// Gets a movie by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The movie identifier.</param>
        /// <returns>The movie with the specified identifier.</returns>
        Task<Movie> GetMovieByIdAsync(int id);

        /// <summary>
        /// Adds a new movie asynchronously.
        /// </summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        Task<Movie> AddMovieAsync(Movie movie);

        /// <summary>
        /// Updates an existing movie asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the movie to update.</param>
        /// <param name="movie">The updated movie data.</param>
        /// <returns>The updated movie.</returns>
        Task<Movie> UpdateMovieAsync(int id, Movie movie);

        /// <summary>
        /// Deletes a movie by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the movie to delete.</param>
        Task DeleteMovieAsync(int id);

        /// <summary>
        /// Checks if a movie exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The movie identifier.</param>
        /// <returns>True if the movie exists, otherwise false.</returns>
        Task<bool> MovieExistsAsync(int id);

        /// <summary>
        /// Updates the characters associated with a movie asynchronously.
        /// </summary>
        /// <param name="movieId">The identifier of the movie.</param>
        /// <param name="characterIds">The list of character identifiers to associate with the movie.</param>
        Task UpdateMovieCharactersAsync(int movieId, List<int> characterIds);

        /// <summary>
        /// Gets the characters associated with a movie asynchronously.
        /// </summary>
        /// <param name="movieId">The identifier of the movie.</param>
        /// <returns>A list of characters in the movie.</returns>
        Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId);
    }

    /// <summary>
    /// Interface for franchise service.
    /// </summary>
    public interface IFranchiseService
    {
        /// <summary>
        /// Gets all franchises asynchronously.
        /// </summary>
        /// <returns>A list of franchises.</returns>
        Task<IEnumerable<Franchise>> GetAllFranchisesAsync();

        /// <summary>
        /// Gets a franchise by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The franchise identifier.</param>
        /// <returns>The franchise with the specified identifier.</returns>
        Task<Franchise> GetFranchiseByIdAsync(int id);

        /// <summary>
        /// Adds a new franchise asynchronously.
        /// </summary>
        /// <param name="franchise">The franchise to add.</param>
        /// <returns>The added franchise.</returns>
        Task<Franchise> AddFranchiseAsync(Franchise franchise);

        /// <summary>
        /// Updates an existing franchise asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the franchise to update.</param>
        /// <param name="franchise">The updated franchise data.</param>
        /// <returns>The updated franchise.</returns>
        Task<Franchise> UpdateFranchiseAsync(int id, Franchise franchise);

        /// <summary>
        /// Deletes a franchise by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the franchise to delete.</param>
        Task DeleteFranchiseAsync(int id);

        /// <summary>
        /// Checks if a franchise exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The franchise identifier.</param>
        /// <returns>True if the franchise exists, otherwise false.</returns>
        Task<bool> FranchiseExistsAsync(int id);

        /// <summary>
        /// Updates the movies associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <param name="movieIds">The list of movie identifiers to associate with the franchise.</param>
        Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movieIds);

        /// <summary>
        /// Gets the movies associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <returns>A list of movies in the franchise.</returns>
        Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int franchiseId);

        /// <summary>
        /// Gets the characters associated with a franchise asynchronously.
        /// </summary>
        /// <param name="franchiseId">The identifier of the franchise.</param>
        /// <returns>A list of characters in the franchise.</returns>
        Task<IEnumerable<Character>> GetCharactersInFranchiseAsync(int franchiseId);
    }
}
