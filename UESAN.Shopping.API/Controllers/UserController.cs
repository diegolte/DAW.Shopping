using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UESAN.Shopping.Core.DTOs;
using UESAN.Shopping.Core.Interfaces;

namespace UESAN.Shopping.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserInsertDTO user)
        {
            var result = await _userService.SignUp(user);
            if (!result)
                return BadRequest();
            return NoContent();
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var retorno = await _userService.SignIn(email, password);
            return Ok(retorno);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAll();
            return Ok(user);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDTO user)
        {
            if (id != user.Id)
                return NotFound();

            var result = await _userService.Update(user);
            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (!result)
                return BadRequest();

            return NoContent();
        }
    }
}
