using Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Concrete.Sql
{
    public static class DatabaseSeeder
    {
        public static void Seed()
        {
            var context = new LibraryDbContext();

            if(!context.Database.GetPendingMigrations().Any())
            {
                if(!context.Books.Any())
                {
                    context.Books.AddRange(Books);
                }

                context.SaveChanges();
            }
        }

        private static readonly Book[] Books = [

            new()
            {
                Id = new Guid(),
                Name = "Pride and Prejudice",
                Description = "Philosophy, history, wit, and the most passionate love story.",
                Author = "Jane Austen",
                ImageUrl = "PrideAndPrejudice.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Id = new Guid(),
                Name = "To Kill a Mockingbird",
                Description = "A jarring & poignantly beautiful story about how humans treat each other.",
                Author = "Harper Lee",
                ImageUrl = "ToKillAMovkingbird.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Id = new Guid(),
                Name = "The Great Gatsby",
                Description = "The greatest, most scathing dissection of the hollowness at the heart of the American dream. Hypnotic, tragic, both of its time and completely relevant.",
                Author = "F. Scott Fitzgerald",
                ImageUrl = "GreatGatsby.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Id = new Guid(),
                Name = "One Hundred Years of Solitude",
                Description = "Magic realism at its best. Both funny and moving, this book made me reflect for weeks on the inexorable march of time.",
                Author = "Gabriel García Márquez",
                ImageUrl = "OneHundredYearsofSolitude.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Id = new Guid(),
                Name = "In Cold Blood",
                Description = "The ‘true crime’ TV show / podcast you’re obsessed with probably owes a debt to this masterpiece of reportage by Truman Capote. Chilling and brilliant.",
                Author = "Truman Capote",
                ImageUrl = "InColdBlood.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            new()
            {
                Id = new Guid(),
                Name = "Hard Times",
                Description = "Pathos, humour, social comment, politic and incredibly well-drawn, believable characters.",
                Author = "Charles Dickens",
                ImageUrl = "HardTimes.jpg",
                IsBorrowed = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
        ];
    }
}
