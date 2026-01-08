using ibop.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ibop.Api.Data
{
    public class IbopDbContext : DbContext
    {
        public IbopDbContext(DbContextOptions<IbopDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: unique email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // -------------------
            // SEED USERS
            // -------------------
            var passwordHasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@ibop.com",
                Role = Role.Admin,
                IsActive = true,
                PasswordHash = passwordHasher.HashPassword(null!, "Admin123!")
            };

            var manager = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Manager",
                LastName = "User",
                Email = "manager@ibop.com",
                Role = Role.Manager,
                IsActive = true,
                PasswordHash = passwordHasher.HashPassword(null!, "Manager123!")
            };

            var staff = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Staff",
                LastName = "User",
                Email = "staff@ibop.com",
                Role = Role.Staff,
                IsActive = true,
                PasswordHash = passwordHasher.HashPassword(null!, "Staff123!")
            };

            modelBuilder.Entity<User>().HasData(admin, manager, staff);
        }
    }
}
