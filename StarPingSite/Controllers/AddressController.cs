using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class AddressController : BaseController<AddressService>
    {
        public AddressController(AddressService service) : base(service) { }
        public async Task<IActionResult> Enter(int orderId)
        {
            var address = await _service.GetByOrderIdAsync(orderId);
            ViewBag.OrderId = orderId;
            return PartialView(address ?? new AddressModel());
        }
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _service.GetByIdAsync(id);
            return PartialView(address ?? new AddressModel());
        }
        public async Task<IActionResult> Save(AddressModel address, int orderId)
        {
            var result = await _service.SaveAsync(address, orderId);
            if (result == false)
                return BadRequest();

            TempData["ActiveTab"] = "Address";
            return RedirectToAction("Details", "Order", new { id = orderId });
        }
    }
}
