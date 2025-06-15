using Microsoft.EntityFrameworkCore;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Services
{
    public class OrderService : BaseService
    {
        public OrderService(StarPingDbContext context) : base(context) { }
        public async Task<OrderModel?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.Where(item => item.Id == id && item.IsActive)
                .FirstOrDefaultAsync();
            return order;
        }
        public async Task<OrderModel?> GetDetailsByIdAsync(int id)
        {
            var order = await _context.Orders.Where(item => item.Id == id && item.IsActive)
                .Include(item => item.Subscriptions)
                .ThenInclude(item => item.Offer)
                .Include(item => item.OrderDevices)
                .ThenInclude(item => item.Device)
                .Include(item => item.Address)
                .Include(item => item.User)
                .Include(item => item.Payments)
                .FirstOrDefaultAsync();
            return order;
        }
        public async Task<IEnumerable<OrderModel>> GetOrdersAsync()
        {
            var orders = await _context.Orders.Where(item => item.IsActive)
                .Include(item => item.Subscriptions)
                .ThenInclude(item => item.Offer)
                .Include(item => item.OrderDevices)
                .ThenInclude(item => item.Device)
                .Include(item => item.Address)
                .Include(item => item.User)
                .Include(item => item.Payments)
                .ToListAsync();
            return orders;
        }
        public async Task<IEnumerable<OrderModel>> GetFinalizedOrdersAsync()
        {
            var orders = await GetOrdersAsync();
            return orders.Where(item => item.IsFinalized);
        }
        public async Task<IEnumerable<OrderModel>> GetNonFinalizedOrdersAsync()
        {
            var orders = await GetOrdersAsync();
            return orders.Where(item => !item.IsFinalized);
        }

        public async Task<OrderModel?> FinalizeAsync(int id)
        {
            var order = await _context.Orders.Where(item => item.Id == id).Include(item => item.Payments).Include(item => item.Address).FirstOrDefaultAsync();
            if (order == null)
                return null;

            if (order.Address != null)
            {
                order.IsFinalized = true;
                order.EditionDate = DateTime.Now;
                var checkoutPayment = order.Payments.Where(item => item.DeadlineDate == null).First();
                if (checkoutPayment != null)
                {
                    checkoutPayment.Status = StarPingData.Helpers.PaymentStatusEnum.Paid;
                    checkoutPayment.PaymentDate = DateTime.Now;
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}
