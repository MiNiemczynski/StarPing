using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarPingData.Models;
using StarPingData.Models.Cart;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CartController : BaseController<CartController>
    {
        private CartService _service;
        public CartController(ILogger<CartController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new CartService(context);
        }

        [HttpPost("{CartSessionId}/Device/{deviceId}")]
        public async Task<IActionResult> AddDeviceToCartAsync(int deviceId, string CartSessionId)
        {
            var device = await _service.AddDeviceToCartAsync(deviceId, CartSessionId);
            if (device == null)
                return NotFound();
            return Ok();
        }
        [HttpPost("{CartSessionId}/Offer/{offerId}/{months}")]
        public async Task<IActionResult> AddSubscriptionToCartAsync(int offerId, int months, string CartSessionId)
        {
            var offer = await _service.AddSubscriptionToCartAsync(offerId, months, CartSessionId);
            if (offer == null)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{CartItemId}")]
        public async Task<IActionResult> DeleteFromCartHandlerAsync(int CartItemId)
        {
            var success = await _service.DeleteFromCartHandlerAsync(CartItemId);
            if (!success)
                return BadRequest();
            return Ok();
        }

        [HttpGet("Items/{CartSessionId}")]
        public async Task<IActionResult> GetCartItemsAsync(string CartSessionId)
        {
            var cartItems = await _service.GetCartItemsAsync(CartSessionId);
            return Ok(cartItems);
        }

        [HttpGet("Total/{CartSessionId}")]
        public async Task<IActionResult> GetTotalAsync(string CartSessionId)
        {
            var total = await _service.GetTotalAsync(CartSessionId);
            return Ok(total);
        }

        [HttpPost("Checkout/{CartSessionId}")]
        public async Task<IActionResult> ConvertToOrderAsync(string CartSessionId)
        {
            var order = await _service.ConvertToOrderAsync(CartSessionId);
            if (order == null)
                return BadRequest();
            return Ok(order);
        }
    }
}