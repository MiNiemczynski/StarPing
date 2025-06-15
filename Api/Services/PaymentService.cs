using Microsoft.EntityFrameworkCore;
using StarPingData.Helpers;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Services
{
    public class PaymentService : BaseService
    {
        public PaymentService(StarPingDbContext context) : base(context) { }
        public async Task<List<PaymentModel>?> GetOrderPaymentsAsync(int orderId)
        {
            var payments = await _context.Payments.Where(item => item.OrderId == orderId && item.IsActive)
                .ToListAsync();
            return payments;
        }

        public async Task<PaymentModel?> PayAsync(int id)
        {
            var payment = await _context.Payments
                .Where(item => item.Id == id && item.IsActive)
                .Include(item => item.Order)
                .FirstOrDefaultAsync();

            if (payment == null)
                return null;

            if (payment.Status == PaymentStatusEnum.Pending && payment.Order.IsFinalized)
            {
                payment.Status = PaymentStatusEnum.Paid;
                payment.PaymentDate = DateTime.Now.Date;
                await _context.SaveChangesAsync();
            }

            return payment;
        }

    }
}
