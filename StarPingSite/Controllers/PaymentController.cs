using Microsoft.AspNetCore.Mvc;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class PaymentController : BaseController<PaymentService>
    {
        public PaymentController(PaymentService service) : base(service) { }
        public async Task<IActionResult> OrderPayments(int orderId)
        {
            var payments = await _service.GetOrderPaymentsAsync(orderId);
            return PartialView(payments);
        }
        public async Task<IActionResult> Pay(int id)
        {
            var result = await _service.PayAsync(id);

            if (result == null)
                return BadRequest();

            TempData["ActiveTab"] = "Payment";
            return RedirectToAction("Details", "Order", new { id = result.OrderId });
        }
    }
}
