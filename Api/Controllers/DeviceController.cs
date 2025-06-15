using Api.Services;
using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DeviceController : BaseController<DeviceController>
    {
        private DeviceService _service;
        public DeviceController(ILogger<DeviceController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new DeviceService(context);
        }

        [HttpGet]
        public async Task<List<DeviceModel>> GetAsync()
        {
            var devices = await _service.GetDevicesAsync();
            return devices;
        }

        [HttpGet("Stationary")]
        public async Task<IActionResult> GetStationaryAsync()
        {
            var stationaryDevices = await _service.GetStationaryAsync();
            return Ok(stationaryDevices);
        }

        [HttpGet("Mobile")]
        public async Task<IActionResult> GetMobileAsync()
        {
            var mobileDevices = await _service.GetMobileAsync();
            return Ok(mobileDevices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int? id)
        {
            var device = await _service.GetByIdAsync(id);
            if (device == null) return NotFound();
            return Ok(device);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAsync(DeviceModel device)
        {
            var result = await _service.SaveAsync(device);
            if (result == null) return BadRequest();
            return Ok(device);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return BadRequest();
            return Ok();
        }
    }
}
