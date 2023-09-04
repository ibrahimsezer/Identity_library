using Identity_library.Domain.DTOS;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly  IProductService _productService;

        public ActionController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet ("GetAllUsers")]
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            // Tüm verileri çekmek için DbContext'i kullanın
            return await _productService.GetAllUsers();
            
        }
        [HttpGet("GetByPhoneNumber")]
        public async Task<IActionResult> GetById(string pnumber)
        {
            
                var product = await _productService.GetByNumber(pnumber);

                if(product != null)
                {
                    return Ok(new Response<IdentityUser>
                    {
                        IsSuccess = true,
                        Data = product
                    }) ;

                }
                else
                {
                    var notFound = new List<string> {"PhoneNumber Not Found" };
                    return NotFound(new Response<IdentityUser>
                    {
                        IsSuccess = false,
                        Errors = notFound

            });
                }
        }

        [HttpPost("token_login")]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginResponse =await _productService.Login(model);
            if (loginResponse.IsSuccess)
            {
                return Ok(loginResponse);
            }

            return BadRequest(loginResponse);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string pnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productService.DeleteUser(pnumber);
            return Ok($"{pnumber} : User Deleted.");
        }

    }
}
