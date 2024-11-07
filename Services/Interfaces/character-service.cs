using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    /// <summary>
    /// Service class for managing characters.
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private readonly MovieCharactersDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CharacterService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all characters asynchronously.
        /// </summary>
        /// <returns>A list of characters.</returns>
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Include(c => c.Movies)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a character by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The character identifier.</param>
        /// <returns>The character with the specified identifier.</returns>
        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Adds a new character asynchronously.
        /// </summary>
        /// <param name="character">The character to add.</param>
        /// <returns>The added character.</returns>
        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Updates an existing character asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the character to update.</param>
        /// <param name="character">The updated character data.</param>
        /// <returns>The updated character.</returns>
        public async Task<Character> UpdateCharacterAsync(int id, Character character)
        {
            var existingCharacter = await _context.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCharacter == null) return null;

            existingCharacter.FullName = character.FullName;
            existingCharacter.Alias = character.Alias;
            existingCharacter.Gender = character.Gender;
            existingCharacter.PictureUrl = character.PictureUrl;

            await _context.SaveChangesAsync();
            return existingCharacter;
        }

        /// <summary>
        /// Deletes a character by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the character to delete.</param>
        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if a character exists by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The character identifier.</param>
        /// <returns>True if the character exists, otherwise false.</returns>
        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }
    }
}
