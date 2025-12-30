using ibop.Api.Data;
using ibop.Api.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IbopDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IbopDbContext context, IPasswordHasher<User> passwordHasher, JwtHelper jwtHelper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtHelper = jwtHelper;
    }

    public async Task<User> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email))
            throw new Exception("User already exists");

        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            throw new Exception("Invalid credentials");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result != PasswordVerificationResult.Success)
            throw new Exception("Invalid credentials");

        return _jwtHelper.GenerateToken(user);
    }
}
