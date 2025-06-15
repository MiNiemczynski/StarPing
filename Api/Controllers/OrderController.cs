using Api.Services;
using Microsoft.AspNetCore.Mvc;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class OrderController : BaseController<OrderController>
    {
        private OrderService _service;
        public OrderController(ILogger<OrderController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new OrderService(context);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
        [HttpGet("Items/{id}")]
        public async Task<IActionResult> GetDetailsByIdAsync(int id)
        {
            var order = await _service.GetDetailsByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await _service.GetOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("Finalized")]
        public async Task<IActionResult> GetFinalizedOrdersAsync()
        {
            var orders = await _service.GetFinalizedOrdersAsync();
            return Ok(orders.Where(item => item.IsFinalized));
        }
        [HttpGet("Nonfinalized")]
        public async Task<IActionResult> GetNonFinalizedOrdersAsync()
        {
            var orders = await _service.GetNonFinalizedOrdersAsync();
            return Ok(orders.Where(item => !item.IsFinalized));
        }

        [HttpPost("Finalize/{id}")]
        public async Task<IActionResult> FinalizeAsync(int id)
        {
            var order = await _service.FinalizeAsync(id);
            if (order == null)
                return NotFound();
            else
                return Ok();
        }
    }
}