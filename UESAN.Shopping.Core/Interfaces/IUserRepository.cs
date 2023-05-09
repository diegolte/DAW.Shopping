using UESAN.Shopping.Core.Entities;

namespace UESAN.Shopping.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> RealSignIn(string email, string password);
        Task<bool> IsEmailRegistered(string email);
        Task<User> SignIn(string email);
        Task<bool> SignUp(User user);
        Task<bool> Update(User user);
    }
}