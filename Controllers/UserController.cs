using Identity_library.Domain.DTOS;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService productService)
        {
            _userService = productService;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            // Tüm verileri çekmek için DbContext'i kullanın
            return await _userService.GetAllUsers();

        }
        [HttpGet("GetByPhoneNumber")]
        public async Task<IActionResult> GetById(string pnumber)
        {

            var product = await _userService.GetByNumber(pnumber);

            if (product != null)
            {
                return Ok(new Response<IdentityUser>
                {
                    IsSuccess = true,
                    Data = product
                });

            }
            else
            {
                var notFound = new List<string> { "PhoneNumber Not Found" };
                return NotFound(new Response<IdentityUser>
                {
                    IsSuccess = false,
                    Errors = notFound

                });
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string pnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.DeleteUser(pnumber);
            return Ok($"{pnumber} : User Deleted.");
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string email,UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdateUser(email,model);
            return Ok($"User Updated. Username : {model.Username} | Email : {model.Email} | Number : {model.PhoneNumber}");
        }
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdatePassword(model);
            return Ok($"Password Changed. New password : {model.NewPassword}");
        }

    }
}
