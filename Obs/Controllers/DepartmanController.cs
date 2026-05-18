using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class DepartmanController : Controller
    {
        private readonly DepartmanApiService _departmanService;

        public DepartmanController(DepartmanApiService departmanService)
        {
            _departmanService = departmanService;
        }

        public async Task<IActionResult> Index()
        {
            var departmanlar = await _departmanService.GetAllAsync();
            return View(departmanlar);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departmans departman)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _departmanService.AddAsync(departman);
                TempData["Mesaj"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(departman);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _departmanService.GetAllAsync();
            var departman = liste.FirstOrDefault(x => x.Id == id);

            if (departman == null)
            {
                return NotFound();
            }

            return View(departman);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Departmans departman)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _departmanService.UpdateAsync(departman);
                TempData["Mesaj"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(departman);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _departmanService.DeleteAsync(id);
            TempData["Mesaj"] = sonuc;
            return RedirectToAction(nameof(Index));
        }
    }
}
