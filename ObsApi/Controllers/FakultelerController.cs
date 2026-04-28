using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakultelerController : ControllerBase
    {
        FakulteSP _sp = new FakulteSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok(_sp.Getir(id));

        [HttpPost]
        public IActionResult Add(Fakulteler f) { _sp.ekle(f); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Fakulteler f) { _sp.guncelle(f); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
