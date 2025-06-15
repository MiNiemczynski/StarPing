using Microsoft.EntityFrameworkCore;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Services
{
    public class DeviceService : BaseService
    {
        public DeviceService(StarPingDbContext context) : base(context) { }
        public async Task<List<DeviceModel>> GetDevicesAsync()
        {
            if (_context?.Devices == null)
            {
                return [];
            }

            var devices = await _context.Devices
                .Where(item => item.IsActive)
                .ToListAsync();

            return devices;
        }

        public async Task<IEnumerable<DeviceModel>> GetStationaryAsync()
        {
            var devices = await GetDevicesAsync();
            var stationaryDevices = devices.Where(item => item.IsMobile == false).OrderByDescending(item => item.Name);
            return stationaryDevices;
        }

        public async Task<IEnumerable<DeviceModel>> GetMobileAsync()
        {
            var devices = await GetDevicesAsync();
            var mobileDevices = devices.Where(item => item.IsMobile == true).OrderBy(item => item.SpeedMbps);
            return mobileDevices;
        }

        public async Task<DeviceModel?> GetByIdAsync(int? id)
        {
            var device = await _context.Devices
                    .Include(item => item.Reviews)
                    .ThenInclude(item => item.User)
                    .FirstOrDefaultAsync(item => item.Id == id);
            return device;
        }
        public async Task<DeviceModel?> SaveAsync(DeviceModel device)
        {
            if (device.Id == 0)
            {
                _context.Devices.Add(device);
                device.IsActive = true;
            }
            else
            {
                var loadedDevice = await _context.Devices.FindAsync(device.Id);
                if (loadedDevice == null)
                    return null;

                loadedDevice.Name = device.Name;
                loadedDevice.Price = device.Price;
                loadedDevice.SpeedMbps = device.SpeedMbps;
                loadedDevice.Description = device.Description;
                loadedDevice.IsMobile = device.IsMobile;
                device = loadedDevice;
            }

            await _context.SaveChangesAsync();
            return device;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                device.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
