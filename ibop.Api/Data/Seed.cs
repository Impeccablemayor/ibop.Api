using ibop.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace ibop.Api.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IbopDbContext context)
        {
            if (context.Users.Any())
                return;

            var hasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@ibop.com",
                Role = Role.Admin,
                IsActive = true,
                PasswordHash = hasher.HashPassword(null!, "Admin123!")
            };

            var manager = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Manager",
                LastName = "User",
                Email = "manager@ibop.com",
                Role = Role.Manager,
                IsActive = true,
                PasswordHash = hasher.HashPassword(null!, "Manager123!")
            };

            var staff = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Staff",
                LastName = "User",
                Email = "staff@ibop.com",
                Role = Role.Staff,
                IsActive = true,
                PasswordHash = hasher.HashPassword(null!, "Staff123!")
            };

            context.Users.AddRange(admin, manager, staff);
            await context.SaveChangesAsync();
        }
    }
}
