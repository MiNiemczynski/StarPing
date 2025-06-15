using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class DevicesController : BaseController<DeviceService>
    {
        public DevicesController(DeviceService service) : base(service) { }
        public async Task<IActionResult> Index()
        {
            var devices = await _service.GetAsync();
            return View(devices.OrderBy(item => item.Name));
        }
        public async Task<IActionResult> Stationary()
        {
            var devices = await _service.GetStationaryAsync();
            return View(devices);
        }
        public async Task<IActionResult> Mobile()
        {
            var devices = await _service.GetMobileAsync();
            return View(devices);
        }
        public async Task<IActionResult> Details(int id)
        {
            var device = await _service.GetByIdAsync(id);
            return View(device);
        }
        public async Task<IActionResult> Save(DeviceModel device)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
                return View("Edit", device?.Id == 0 ? null : device);

            var loadedDevice = await _service.SaveAsync(device);
            if (loadedDevice == null) return BadRequest();
            return RedirectToAction("Details", new { id = loadedDevice.Id });
        }

        public IActionResult Create()
        {
            return View("Edit");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var device = await _service.GetByIdAsync(id);
            return View(device);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            if (!response) return BadRequest();
            return RedirectToAction("Index");
        }
    }
}
