using ibop.Api.Data;
using ibop.Api.DTOs;
using ibop.Api.DTOs.Auth;
using ibop.Api.Entities;
using ibop.Api.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ibop.Api.Services
{
    public class AuthService
    {
        private readonly IbopDbContext _db;
        private readonly JwtHelper _jwt;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IbopDbContext db, JwtHelper jwt)
        {
            _db = db;
            _jwt = jwt;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower() && u.IsActive);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return null;

            var token = _jwt.GenerateToken(
                user.Id,
                user.Email,
                user.Role.ToString()
            );

            return new LoginResponseDto
            {
                Token = token,
                Role = user.Role.ToString(),
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }
    }
}
