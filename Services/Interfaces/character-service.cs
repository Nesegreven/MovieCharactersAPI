using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services.Interfaces;

namespace MovieCharactersAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieCharactersDbContext _context;

        public CharacterService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Include(c => c.Movies)
                .ToListAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<Character> UpdateCharacterAsync(int id, Character character)
        {
            var existingCharacter = await _context.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCharacter == null)
                return null;

            existingCharacter.FullName = character.FullName;
            existingCharacter.Alias = character.Alias;
            existingCharacter.Gender = character.Gender;
            existingCharacter.PictureUrl = character.PictureUrl;

            await _context.SaveChangesAsync();
            return existingCharacter;
        }

        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }
    }
}
