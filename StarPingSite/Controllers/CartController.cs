using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingData.Models.Cart;
using StarPingData.Models.Context;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class CartController : BaseController<WebCartService>
    {
        public CartController(WebCartService service) : base(service) { }
        public async Task<IActionResult> Index()
        {
            var dataForCart = await _service.GetCartData();
            return View(dataForCart);
        }
        public async Task<IActionResult> AddDeviceToCart(int id, bool finalize = false)
        {
            var success = await _service.AddDeviceToCart(id);

            if(!success)
                return NotFound();

            if (finalize)
                return await CreateOrderAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddOfferToCart(int id, int months, bool finalize = false)
        {
            if (months <= 0)
                return BadRequest("Please, choose subscription period");

            var success = await _service.AddSubscriptionToCart(id, months);

            if (!success)
                return NotFound();

            if (finalize)
                return await CreateOrderAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var success = await _service.DeleteItemFromCartAsync(id);

            if (!success)
                return NotFound();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateOrderAsync()
        {
            OrderModel? order = await _service.CreateOrderAsync();

            if (order == null)
                return BadRequest();

            return RedirectToAction("Details", "Order", new { id = order.Id });
        }
    }
}
