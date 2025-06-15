using Microsoft.AspNetCore.Mvc;

namespace StarPingSite.Controllers
{
    public abstract class BaseController<ControllerService> : Controller
    {
        protected readonly ControllerService _service;
        protected BaseController(ControllerService service)
        {
            _service = service;
        }
    }
}
