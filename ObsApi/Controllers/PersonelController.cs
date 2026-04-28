using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonelController : ControllerBase
    {
        PersonelSP _sp = new PersonelSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok(_sp.Getir(id));

        [HttpPost]
        public IActionResult Add(Personel p) { _sp.ekle(p); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Personel p) { _sp.guncelle(p); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
