using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UESAN.Shopping.Core.DTOs;
using UESAN.Shopping.Core.Entities;
using UESAN.Shopping.Core.Interfaces;

namespace UESAN.Shopping.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> SignUp(UserInsertDTO userInsertDTO)
        {
            var user = new User();

            user.FirstName = userInsertDTO.FirstName;
            user.LastName = userInsertDTO.LastName;
            user.DateOfBirth = userInsertDTO.DateOfBirth;
            user.Country = userInsertDTO.Country;
            user.Address = userInsertDTO.Address;
            user.Email = userInsertDTO.Email;
            user.Password = userInsertDTO.Password;
            user.IsActive = true;
            user.Type = userInsertDTO.Type;

            var result = await _userRepository.SignUp(user);
            return result;
        }

        public async Task<String> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email);
            if (user == null)
                return "Correo no encontrado";
            else if (user.IsActive != true)
                return "Usuario inactivo";
            else if (user.Password != password)
                return "Contraseña incorrecta";
            return "Acceso exitoso";

        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var user = await _userRepository.GetAll();
            var userDTO = user.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Address = u.Address,
                Country = u.Country,
                Email = u.Email,
                Password = u.Password,
                isActive = u.IsActive,
                Type = u.Type
            });
            return userDTO;
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return null;

            var userDTO = new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                Country = user.Country,
                Email = user.Email,
                Password = user.Password,
                isActive = user.IsActive,
                Type = user.Type
            };
            return userDTO;
        }

        public async Task<bool> Update(UserUpdateDTO userUpdateDTO)
        {
            var user = await _userRepository.GetById(userUpdateDTO.Id);
            if (user == null)
                return false;
            user.FirstName = userUpdateDTO.FirstName;
            user.LastName = userUpdateDTO.LastName;
            user.DateOfBirth = userUpdateDTO.DateOfBirth;
            user.Address = userUpdateDTO.Address;
            user.Country = userUpdateDTO.Country;
            user.Email = userUpdateDTO.Email;
            user.Password = userUpdateDTO.Password;
            user.Type = userUpdateDTO.Type;

            var result = await _userRepository.Update(user);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return false;

            var result = await _userRepository.Delete(id);
            return result;
        }
    }
}
