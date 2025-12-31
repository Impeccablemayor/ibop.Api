using ibop.Api.Data;
using ibop.Api.DTOs;
using ibop.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ibop.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IbopDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IbopDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // ✅ Correct password hashing
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task ActivateAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return;

            user.IsActive = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return;

            user.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task ResetPasswordAsync(int id, string newPassword)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return;

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            await _context.SaveChangesAsync();
        }
    }
}
