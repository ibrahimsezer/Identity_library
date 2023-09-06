using Identity_library.Domain.Interface;
using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            var address = await _addressService.GetAllAddress();
            return Ok(address);
        }
        [HttpGet("GetIsActive")]
        public async Task<IActionResult> GetActiveAddress()
        {
            var activeAddress = await _addressService.GetActiveAddress();
            return Ok(activeAddress);
        }
        [HttpPost("CreateAddress")]

        public async Task<IActionResult> CreateAddress(UserAddress model)
        {
            var address = await _addressService.CreateAddress(model);
            return Ok("Address created");
        }
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(UserAddress model)
        {
            var address = await _addressService.UpdateAddress(model);
            return Ok($"Address updated. New address : {address.Title} | {address.Address}");
        }
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressService.DeleteAddress(id);
            return Ok("Address deleted.");
        }
    }
}
