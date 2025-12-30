using ibop.Api.Data;
using ibop.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ibop.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IbopDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(IbopDbContext context)
        {
            _context = context;
        }

        public User Create(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) => _context.Users.Find(id);

        public void Activate(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.IsActive = true;
                _context.SaveChanges();
            }
        }

        public void Deactivate(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveChanges();
            }
        }

        public void ResetPassword(int id, string newPassword)
        {
            var user = GetById(id);
            if (user != null)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
                _context.SaveChanges();
            }
        }
    }
}
