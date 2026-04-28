using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelDetayController : ControllerBase
    {
        private readonly PersonelDetayVMSP _service;

        public PersonelDetayController(PersonelDetayVMSP service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.PersonelListe());
        }
    }
}
