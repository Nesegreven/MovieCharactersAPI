using MovieCharactersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieCharactersAPI.Data.Helpers
{
    public static class DbInitializer
    {
        public static async Task Initialize(MovieCharactersDbContext context)
        {
            // Check if database has any data
            if (context.Movies.Any())
                return;   // DB has been seeded

            // 1. FRANCHISES
            var franchises = new List<Franchise>
            {
                new Franchise
                {
                    Name = "Marvel Cinematic Universe",
                    Description = "Superhero film franchise produced by Marvel Studios"
                },
                new Franchise
                {
                    Name = "The Lord of the Rings",
                    Description = "Epic high-fantasy film series directed by Peter Jackson, based on J.R.R. Tolkien's novels"
                }
            };

            await context.Franchises.AddRangeAsync(franchises);
            await context.SaveChangesAsync();

            // 2. CHARACTERS
            var characters = new List<Character>
            {
                // Marvel Characters
                new Character
                {
                    FullName = "Tony Stark",
                    Alias = "Iron Man",
                    Gender = "Male",
                    PictureUrl = "https://example.com/ironman.jpg"
                },
                new Character
                {
                    FullName = "Thor Odinson",
                    Alias = "God of Thunder",
                    Gender = "Male",
                    PictureUrl = "https://example.com/thor.jpg"
                },

                // Lord of the Rings Characters
                new Character
                {
                    FullName = "Frodo Baggins",
                    Alias = "Ring-bearer",
                    Gender = "Male",
                    PictureUrl = "https://example.com/frodo.jpg"
                },
                new Character
                {
                    FullName = "Gandalf",
                    Alias = "Mithrandir",
                    Gender = "Male",
                    PictureUrl = "https://example.com/gandalf.jpg"
                },
                new Character
                {
                    FullName = "Aragorn",
                    Alias = "Strider",
                    Gender = "Male",
                    PictureUrl = "https://example.com/aragorn.jpg"
                },
                new Character
                {
                    FullName = "Legolas",
                    Alias = "Prince of the Woodland Realm",
                    Gender = "Male",
                    PictureUrl = "https://example.com/legolas.jpg"
                },
                new Character
                {
                    FullName = "Samwise Gamgee",
                    Alias = "Sam",
                    Gender = "Male",
                    PictureUrl = "https://example.com/sam.jpg"
                }
            };

            await context.Characters.AddRangeAsync(characters);
            await context.SaveChangesAsync();

            // 3. MOVIES
            var movies = new List<Movie>
            {
                // Marvel Movie
                new Movie
                {
                    MovieTitle = "Iron Man",
                    Genre = "Action, Sci-Fi",
                    ReleaseYear = 2008,
                    Director = "Jon Favreau",
                    PictureUrl = "https://example.com/ironman-poster.jpg",
                    TrailerUrl = "https://example.com/ironman-trailer",
                    FranchiseId = franchises[0].Id,
                    Characters = new List<Character> { characters[0], characters[1] }  // Tony and Thor
                },

                // Lord of the Rings Movies
                new Movie
                {
                    MovieTitle = "The Fellowship of the Ring",
                    Genre = "Fantasy, Adventure",
                    ReleaseYear = 2001,
                    Director = "Peter Jackson",
                    PictureUrl = "https://example.com/lotr1-poster.jpg",
                    TrailerUrl = "https://example.com/lotr1-trailer",
                    FranchiseId = franchises[1].Id,
                    Characters = new List<Character>
                    {
                        characters[2], // Frodo
                        characters[3], // Gandalf
                        characters[4], // Aragorn
                        characters[5], // Legolas
                        characters[6]  // Sam
                    }
                },
                new Movie
                {
                    MovieTitle = "The Two Towers",
                    Genre = "Fantasy, Adventure",
                    ReleaseYear = 2002,
                    Director = "Peter Jackson",
                    PictureUrl = "https://example.com/lotr2-poster.jpg",
                    TrailerUrl = "https://example.com/lotr2-trailer",
                    FranchiseId = franchises[1].Id,
                    Characters = new List<Character>
                    {
                        characters[2], // Frodo
                        characters[3], // Gandalf
                        characters[4], // Aragorn
                        characters[5], // Legolas
                        characters[6]  // Sam
                    }
                },
                new Movie
                {
                    MovieTitle = "The Return of the King",
                    Genre = "Fantasy, Adventure",
                    ReleaseYear = 2003,
                    Director = "Peter Jackson",
                    PictureUrl = "https://example.com/lotr3-poster.jpg",
                    TrailerUrl = "https://example.com/lotr3-trailer",
                    FranchiseId = franchises[1].Id,
                    Characters = new List<Character>
                    {
                        characters[2], // Frodo
                        characters[3], // Gandalf
                        characters[4], // Aragorn
                        characters[5], // Legolas
                        characters[6]  // Sam
                    }
                }
            };

            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }
    }
}