using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiniflarController : ControllerBase
    {
        SinifSP _sp = new SinifSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok(_sp.Getir(id));

        [HttpPost]
        public IActionResult Add(Siniflar s) { _sp.ekle(s); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Siniflar s) { _sp.guncelle(s); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
