using Identity_library.Interface;
using Identity_library.Models;
using Identity_library.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpGet("GetByPhoneNumber")]
        public async Task<IActionResult> GetById(string pnumber)
        {
            try
            {
                var product = _productService.GetProductByPnumber(pnumber);

                if(product != null)
                {
                    return Ok(new ApiResponse<IdentityUser>
                    {
                        Success = true,
                        Data = product
                    }) ;

                }
                else
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        ErrorMessage = "Phone number not found."
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("token_login")]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            var loginResponse = _productService.Login(model);
            if (loginResponse.Success)
            {
                return Ok(loginResponse);
            }

            return BadRequest(loginResponse);
        }

    }
}
