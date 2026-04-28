using Microsoft.AspNetCore.Mvc;
using isKatmani;
namespace ObsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciDersNotlari : ControllerBase
    {
        private readonly isKatmani.OgrenciDersNotlariVMSP _service;

        public OgrenciDersNotlari(OgrenciDersNotlariVMSP service)
        {
            _service = service;
        }

        [HttpGet("notlar")]
        public IActionResult Notlar(int? ogrenciId)
        {
            var data = _service.OgrenciNotlariniGetir(ogrenciId);
            return Ok(data);
        }
    }
}
