using UESAN.Shopping.Core.DTOs;

namespace UESAN.Shopping.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<string> SignIn(string email, string password);
        Task<bool> SignUp(UserInsertDTO userInsertDTO);
        Task<bool> Update(UserUpdateDTO userUpdateDTO);
    }
}