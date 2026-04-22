using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace ObsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        isKatmani.RolesSP _roles = new isKatmani.RolesSP();

        // 🔹 GET: api/roles
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roles.Liste());
        }

        // 🔹 POST: api/roles
        [HttpPost]
        public IActionResult Add(Roles r)
        {
            _roles.ekle(r);
            return Ok("Eklendi");
        }

        // 🔹 PUT: api/roles
        [HttpPut]
        public IActionResult Update(Roles r)
        {
            //_roles. .guncelle(r);
            return Ok("Rolede Güncelleme yok");
        }

        // 🔹 DELETE: api/roles/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roles.sil_id_ile(id);
            return Ok("Silindi");
        }
    }
}
