using Microsoft.EntityFrameworkCore;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Services
{
    public class OfferService : BaseService
    {
        public OfferService(StarPingDbContext context) : base(context) { }
        public async Task<List<OfferModel>> GetOffersAsync()
        {
            if (_context?.Offers == null)
            {
                return [];
            }

            var offers = await _context.Offers
                .Where(item => item.IsActive)
                .ToListAsync();

            return offers;
        }

        public async Task<IEnumerable<OfferModel>> GetStationaryAsync()
        {
            var offers = await GetOffersAsync();
            var stationaryOffers = offers.Where(item => item.IsMobile == false).OrderByDescending(item => item.Name);
            return stationaryOffers;
        }

        public async Task<IEnumerable<OfferModel>> GetMobileAsync()
        {
            var offers = await GetOffersAsync();
            var mobileOffers = offers.Where(item => item.IsMobile == true).OrderBy(item => item.SpeedMbps);
            return mobileOffers;
        }

        public async Task<IEnumerable<OfferModel>> GetBusinessAsync()
        {
            var offers = await GetOffersAsync();
            var businessOffers = offers.Where(item => item.Name.Contains("Business")).OrderBy(item => item.SpeedMbps);
            return businessOffers;
        }

        public async Task<OfferModel?> GetByIdAsync(int? id)
        {
            var offer = await _context.Offers
                    .Include(item => item.Reviews)
                    .ThenInclude(item => item.User)
                    .FirstOrDefaultAsync(item => item.Id == id);
            return offer;
        }
        public async Task<OfferModel?> SaveAsync(OfferModel offer)
        {
            if (offer.Id == 0)
            {
                _context.Offers.Add(offer);
                offer.IsActive = true;
            }
            else
            {
                var loadedOffer = await _context.Offers.FindAsync(offer.Id);
                if (loadedOffer == null)
                    return null;

                loadedOffer.Name = offer.Name;
                loadedOffer.Price = offer.Price;
                loadedOffer.SpeedMbps = offer.SpeedMbps;
                loadedOffer.Description = offer.Description;
                loadedOffer.IsMobile = offer.IsMobile;
                offer = loadedOffer;
            }

            await _context.SaveChangesAsync();
            return offer;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                offer.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
