using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<LoggerController> : Controller
    {
        protected readonly ILogger<LoggerController> _logger;
        public BaseController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }
    }
}
