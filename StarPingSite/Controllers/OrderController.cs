using Microsoft.AspNetCore.Mvc;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class OrderController : BaseController<OrderService>
    {
        public OrderController(OrderService service) : base(service) { }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            return View(order);
        }
        public async Task<IActionResult> OrderItems(int id)
        {
            var orderItems = await _service.GetOrderDetailsByIdAsync(id);

            if (orderItems == null)
                return NotFound();

            return PartialView(orderItems);
        }
        public async Task<IActionResult> All()
        {
            var orders = await _service.GetOrdersAsync();
            return View(orders);
        }
        public async Task<IActionResult> Finalize(int id)
        {
            var orders = await _service.GetOrderDetailsByIdAsync(id);
            return PartialView(orders);
        }
        public async Task<IActionResult> Save(int id)
        {
            var result = await _service.FinalizeAsync(id);

            if (result == false)
                return BadRequest();

            TempData["ActiveTab"] = "Finalize";
            return RedirectToAction("Details", "Order", new { id });
        }
    }
}
