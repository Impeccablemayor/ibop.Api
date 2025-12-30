using ibop.Api.Entities;

namespace ibop.Api.Services
{
    public interface IUserService
    {
        User? Authenticate(string email, string password);
        User Create(User user, string password);
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Activate(int id);
        void Deactivate(int id);
        void ResetPassword(int id, string newPassword);
    }
}
