using Microsoft.EntityFrameworkCore;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Services
{
    public class AddressService : BaseService
    {
        public AddressService(StarPingDbContext context) : base(context) { }
        public async Task<AddressModel?> GetByOrderIdAsync(int orderId)
        {
            var address = await _context.Orders
                .Where(item => item.Id == orderId)
                .Select(item => item.Address)
                .FirstOrDefaultAsync();
            return address;
        }

        public async Task<AddressModel?> GetByIdAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            return address;
        }

        public async Task<AddressModel?> SaveAsync(AddressModel address, int orderId = 0)
        {
            if (address.Id == 0)
            {
                address.IsActive = true;
                _context.Addresses.Add(address);
            }
            else
            {
                var loadedAddress = await _context.Addresses.FindAsync(address.Id);
                if (loadedAddress == null)
                    return null;

                loadedAddress.Street = address.Street;
                loadedAddress.City = address.City;
                loadedAddress.PostalCode = address.PostalCode;
                loadedAddress.Region = address.Region;

                address = loadedAddress;
            }
            if (orderId != 0)
            {
                var order = await _context.Orders.FirstOrDefaultAsync(item => item.Id == orderId);
                if (order == null)
                    return null;
                order.Address = address;
            }
            await _context.SaveChangesAsync();
            return address;
        }
    }
}
