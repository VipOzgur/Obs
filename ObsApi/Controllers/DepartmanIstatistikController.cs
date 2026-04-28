using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmanIstatistikController : ControllerBase
    {
        private readonly DepartmanIstatistikVMSP _service;

        public DepartmanIstatistikController(DepartmanIstatistikVMSP service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.DepartmanIstatistik());
        }
    }
}
