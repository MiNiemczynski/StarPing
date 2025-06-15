using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingSite.Services;

namespace StarPingSite.Controllers
{
    public class OffersController : BaseController<OfferService>
    {
        public OffersController(OfferService service) : base(service) { }
        public async Task<IActionResult> Index()
        {
            var offers = await _service.GetAsync();
            return View(offers.OrderBy(item => item.Name));
        }
        public async Task<IActionResult> Stationary()
        {
            var offers = await _service.GetStationaryAsync();
            return View(offers);
        }
        public async Task<IActionResult> Mobile()
        {
            var offers = await _service.GetMobileAsync();
            return View(offers);
        }
        public async Task<IActionResult> Business()
        {
            var offers = await _service.GetBusinessAsync();
            return View(offers);
        }
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _service.GetByIdAsync(id);
            return View(offer);
        }
        public async Task<IActionResult> Save(OfferModel offer)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
                return View("Edit", offer?.Id == 0 ? null : offer);

            var loadedOffer = await _service.SaveAsync(offer);
            if (loadedOffer == null) return BadRequest();
            return RedirectToAction("Details", new { id = loadedOffer.Id });
        }

        public IActionResult Create()
        {
            return View("Edit");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _service.GetByIdAsync(id);
            return View(offer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            if (!response) return BadRequest();
            return RedirectToAction("Index");
        }
    }
}
