using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmansController : ControllerBase
    {
        isKatmani.DepartmansSP _sp = new isKatmani.DepartmansSP();

        [HttpGet]
        public IActionResult GetAll() => Ok(_sp.Liste());

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id) => Ok(_sp. (id));

        [HttpPost]
        public IActionResult Add(Departmans d) { _sp.ekle(d); return Ok("Eklendi"); }

        [HttpPut]
        public IActionResult Update(Departmans d) { _sp.guncelle(d); return Ok("Güncellendi"); }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { _sp.sil(id); return Ok("Silindi"); }
    }
}
