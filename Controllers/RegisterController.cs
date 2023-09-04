using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IRegisterService _registerService;
        public RegisterController(IRegisterService registerService)
        {

            _registerService = registerService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Response<RegisterModel> result = await _registerService.RegisterAsyncService(model);

                if (result.IsSuccess)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }
            return BadRequest(ModelState);
        }
    }
}