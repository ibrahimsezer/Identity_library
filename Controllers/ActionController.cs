using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPost("token_login")]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginResponse = _productService.Login(model);
            if (loginResponse.Success)
            {
                return Ok(loginResponse);
            }

            return BadRequest(loginResponse);
        }

    }
}
