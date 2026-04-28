using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DerslerController : ControllerBase
    {
        DerslerSP _sp = new DerslerSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok(_sp.Getir(id));

        [HttpPost]
        public IActionResult Add(Dersler d) { _sp.ekle(d); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Dersler d) { _sp.guncelle(d); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
