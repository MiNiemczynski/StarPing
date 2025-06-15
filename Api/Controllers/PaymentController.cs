using Api.Services;
using Microsoft.AspNetCore.Mvc;
using StarPingData.Models.Context;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class PaymentController : BaseController<PaymentController>
    {
        private PaymentService _service;
        public PaymentController(ILogger<PaymentController> logger, StarPingDbContext context) : base(logger)
        {
            _service = new PaymentService(context);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderPaymentsAsync(int orderId)
        {
            var payments = await _service.GetOrderPaymentsAsync(orderId);
            return Ok(payments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PayAsync(int id)
        {
            var payment = await _service.PayAsync(id);
            return Ok(payment);
        }
    }
}
