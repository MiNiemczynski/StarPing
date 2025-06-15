using Api.Services;
using Microsoft.AspNetCore.Mvc;
using StarPingData.Models;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class OfferController : BaseController<OfferController>
    {
        private OfferService _service;
        public OfferController(ILogger<OfferController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new OfferService(context);
        }

        [HttpGet]
        public async Task<List<OfferModel>> GetAsync()
        {
            var offers = await _service.GetOffersAsync();
            return offers;
        }

        [HttpGet("Stationary")]
        public async Task<IActionResult> GetStationaryAsync()
        {
            var stationaryOffers = await _service.GetStationaryAsync();
            return Ok(stationaryOffers);
        }

        [HttpGet("Mobile")]
        public async Task<IActionResult> GetMobileAsync()
        {
            var mobileOffers = await _service.GetMobileAsync();
            return Ok(mobileOffers);
        }

        [HttpGet("Business")]
        public async Task<IActionResult> GetBusinessAsync()
        {
            var businessOffers = await _service.GetBusinessAsync();
            return Ok(businessOffers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int? id)
        {
            var offer = await _service.GetByIdAsync(id);
            if (offer == null) return NotFound();
            return Ok(offer);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAsync(OfferModel offer)
        {
            var result = await _service.SaveAsync(offer);
            if (result == null) return BadRequest();
            return Ok(offer);
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
