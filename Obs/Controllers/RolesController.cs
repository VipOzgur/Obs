using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "SuperAdminOnly")]
    public class RolesController : Controller
    {
        private readonly RolesApiService _rolesService;

        public RolesController(RolesApiService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task<IActionResult> Index()
        {
            var roller = await _rolesService.GetAllAsync();
            return View(roller);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roles role)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _rolesService.AddAsync(role);
                TempData["Mesaj"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _rolesService.GetAllAsync();
            var role = liste.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Roles role)
        {
            var sonuc = await _rolesService.UpdateAsync(role);
            TempData["Mesaj"] = sonuc;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _rolesService.DeleteAsync(id);
            TempData["Mesaj"] = sonuc;
            return RedirectToAction(nameof(Index));
        }
    }
}
