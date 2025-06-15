using Api.Services;
using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AddressController : BaseController<AddressController>
    {
        private AddressService _service;
        public AddressController(ILogger<AddressController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new AddressService(context);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderIdAsync(int orderId)
        {
            var address = await _service.GetByOrderIdAsync(orderId);
            return Ok(address);
        }

        [HttpGet("address/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var address = await _service.GetByIdAsync(id);
            return Ok(address);
        }
        [HttpPost("save")]
        public async Task<IActionResult> SaveAsync(AddressModel address, int orderId = 0)
        {
            var result = await _service.SaveAsync(address, orderId);
            if (result == null) return BadRequest();
            return Ok();
        }
    }
}
