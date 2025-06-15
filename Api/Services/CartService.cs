using Microsoft.EntityFrameworkCore;
using StarPingData.Helpers;
using StarPingData.Models;
using StarPingData.Models.Cart;
using StarPingData.Models.Context;
using StarPingSite.Services;

namespace Api.Services
{
    public class CartService : BaseService
    {
        public CartService(StarPingDbContext _context) : base(_context) { }

        public async Task<DeviceModel?> AddDeviceToCartAsync(int deviceId, string CartSessionId)
        {
            var device = await _context.Devices.Where(item => item.Id == deviceId && item.IsActive).FirstOrDefaultAsync();
            if(device == null)
            {
                return null;
            }

            var existingCartItem = await _context.CartItems.Where(item => item.IdSessionOfCart == CartSessionId
                                                            && item.DeviceId == deviceId
                                                            && item.IsActive)
                                                            .FirstOrDefaultAsync();
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                var cartItem = new CartItemModel
                {
                    DeviceId = deviceId,
                    IdSessionOfCart = CartSessionId,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<OfferModel?> AddSubscriptionToCartAsync(int offerId, int months, string CartSessionId)
        {
            var offer = await _context.Offers.Where(item => item.Id == offerId && item.IsActive).FirstOrDefaultAsync();
            if (offer == null)
            {
                return null;
            }

            var existingCartItem = await _context.CartItems.Where(item => item.IdSessionOfCart == CartSessionId
                                                            && item.Subscription.OfferId == offerId
                                                            && item.Subscription.Months == months
                                                            && item.IsActive)
                                                            .FirstOrDefaultAsync();
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                SubscriptionModel subscription = new()
                {
                    OfferId = offerId,
                    Months = months,
                    CreationDate = DateTime.Now,
                    IsActive = true
                };
                await _context.Subscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();

                var cartItem = new CartItemModel
                {
                    SubscriptionId = subscription.Id,
                    IdSessionOfCart = CartSessionId,
                    IsSubscription = true,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<bool> DeleteFromCartHandlerAsync(int CartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(CartItemId);
            if (cartItem != null)
            {
                if (cartItem.Quantity == 1)
                    cartItem.IsActive = false;
                else
                    cartItem.Quantity--;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CartItemModel>> GetCartItemsAsync(string CartSessionId)
        {
            return await _context.CartItems
                    .Where(item => item.IdSessionOfCart == CartSessionId && item.IsActive)
                    .Include(item => item.Device)
                    .Include(item => item.Subscription)
                    .ThenInclude(item => item.Offer)
                    .ToListAsync();
        }
        public async Task<Dictionary<int, decimal>> GetTotalAsync(string CartSessionId)
        {
            Dictionary<int, decimal> priceDictionary = [];
            var devicesTotal = await _context.CartItems
                .Where(item => item.IdSessionOfCart == CartSessionId && !item.IsSubscription && item.IsActive)
                .Select(item => item.Device.Price * item.Quantity)
                .SumAsync();
            priceDictionary.Add(0, devicesTotal);

            var cartSubscriptions = await _context.CartItems
                .Where(item => item.IdSessionOfCart == CartSessionId && item.IsSubscription && item.IsActive)
                .Include(item => item.Subscription)
                .ThenInclude(item => item.Offer)
                .ToListAsync();

            foreach (var cartItem in cartSubscriptions)
            {
                if (!priceDictionary.ContainsKey(cartItem.Subscription.Months))
                    priceDictionary[cartItem.Subscription.Months] = 0;
                priceDictionary[cartItem.Subscription.Months] += cartItem.Subscription.Offer.Price * cartItem.Quantity;
            }

            return priceDictionary;
        }

        public async Task<OrderModel> ConvertToOrderAsync(string CartSessionId)
        {
            List<CartItemModel> cartItems = await GetCartItemsAsync(CartSessionId);
            OrderModel order = new();
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                order.CreationDate = DateTime.Now;
                order.IsActive = true;
                await _context.AddAsync(order);
                await _context.SaveChangesAsync();
                decimal devicesPrice = 0;
                foreach (var cartItem in cartItems.Where(item => !item.IsSubscription))
                {
                    for (int q = 1; q <= cartItem.Quantity; q++)
                    {
                        var orderDevice = new OrderDeviceModel
                        {
                            OrderId = order.Id,
                            DeviceId = cartItem.Device.Id,
                            CreationDate = DateTime.Now,
                            IsActive = true
                        };
                        await _context.AddAsync(orderDevice);
                        devicesPrice += cartItem.Device.Price;
                    }
                }
                var devicesPayment = new PaymentModel
                {
                    OrderId = order.Id,
                    Amount = devicesPrice,
                    Status = PaymentStatusEnum.Pending,
                    CreationDate = DateTime.Now.Date,
                    IsActive = true
                };
                await _context.AddAsync(devicesPayment);

                Dictionary<int, decimal> monthlyPayments = [];
                foreach (var cartItem in cartItems.Where(item => item.IsSubscription))
                {
                    for (int q = 1; q <= cartItem.Quantity; q++)
                    {
                        var orderSubscription = new SubscriptionModel
                        {
                            OrderId = order.Id,
                            OfferId = cartItem.Subscription.OfferId,
                            Offer = await _context.Offers.FindAsync(cartItem.Subscription.OfferId),
                            Months = cartItem.Subscription.Months,
                            CreationDate = DateTime.Now,
                            IsActive = true
                        };
                        await _context.AddAsync(orderSubscription);

                        for (int month = 1; month <= cartItem.Subscription.Months; month++)
                        {
                            if (!monthlyPayments.ContainsKey(month))
                                monthlyPayments[month] = 0;
                            monthlyPayments[month] += cartItem.Subscription.Offer.Price;
                        }
                    }
                }
                foreach (var monthEntry in monthlyPayments)
                {
                    int offset = monthEntry.Key;
                    decimal monthTotal = monthEntry.Value;

                    var payment = new PaymentModel
                    {
                        OrderId = order.Id,
                        Amount = monthTotal,
                        Status = PaymentStatusEnum.Pending,
                        CreationDate = DateTime.Now,
                        DeadlineDate = DateTime.Now.Date.AddMonths(offset),
                        IsActive = true
                    };

                    await _context.AddAsync(payment);
                }

                // Usuwanie z koszyka
                foreach (var cartItem in cartItems)
                    cartItem.IsActive = false;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            return order;
        }
    }
}
