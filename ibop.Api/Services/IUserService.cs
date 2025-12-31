using ibop.Api.DTOs;
using ibop.Api.Entities;

namespace ibop.Api.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string email, string password);
        Task<User> CreateAsync(CreateUserDto dto);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task ActivateAsync(int id);
        Task DeactivateAsync(int id);
        Task ResetPasswordAsync(int id, string newPassword);
    }
}
