using LMS.Application.Helpers;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Enums;

namespace LMS.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            // Seed users
            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(
                    new User
                    {
                        Email = "test@yopmail.com",
                        Username = "testuser_1",
                        Password = Utils.HashPassword("Password123$"),
                        RecordStatus = RecordStatus.Active,
                        Role = Role.User
                    },
                    new User
                    {
                        Email = "test_2@yopmail.com",
                        Username = "testuser_2",
                        Password = Utils.HashPassword("Password12345$"),
                        RecordStatus = RecordStatus.Active,
                        Role = Role.User
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed books
            if (!await context.Books.AnyAsync())
            {
                await context.Books.AddRangeAsync(
                    new Book
                    {
                        Title = "Harry Porter",
                        Author = "John Doe",
                        ISBN = "11w6262-287277-12tt21-1111",
                        RecordStatus = RecordStatus.Active,
                        PublishedDate = DateTime.UtcNow,
                    },
                     new Book
                     {
                         Title = "Book Factory",
                         Author = "John Hopkins",
                         ISBN = "11w6262-11111-20000-ABCD",
                         RecordStatus = RecordStatus.Active,
                         PublishedDate = DateTime.UtcNow,
                     }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
