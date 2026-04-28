 using Microsoft.AspNetCore.Mvc;
using isKatmani;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OgrenciController : ControllerBase
    {
        isKatmani.OgrenciSP _ogrenciSP = new isKatmani.OgrenciSP();

        // 🔹 GET: api/ogrenci
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ogrenciSP.Liste());
        }

        // 🔹 POST: api/ogrenci
        [HttpPost]
        public IActionResult Add(Ogrenci o)
        {
            _ogrenciSP.ekle(o);
            return Ok("Eklendi");
        }

        // 🔹 PUT: api/ogrenci
        [HttpPut]
        public IActionResult Update(Ogrenci o)
        {
            _ogrenciSP.guncelle(o);
            return Ok("Güncellendi");
        }

        // 🔹 DELETE: api/ogrenci/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ogrenciSP.sil(id);
            return Ok("Silindi");
        }
    }
}
