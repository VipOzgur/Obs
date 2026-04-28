using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        LogSP _sp = new LogSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        [HttpPost]
        public IActionResult Add(Log l) { _sp.ekle(l); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Log l) { _sp.guncelle(l); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
