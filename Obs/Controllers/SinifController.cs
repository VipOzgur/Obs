using ApiLayer.ObsWeb.Services;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class SinifController : Controller
    {
        private readonly SinifApiService _sinifService;

        public SinifController(SinifApiService sinifService)
        {
            _sinifService = sinifService;
        }

        public async Task<IActionResult> Index()
        {
            var siniflar = await _sinifService.GetAllAsync();
            return View(siniflar);
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
        public async Task<IActionResult> Create(Siniflar sinif)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _sinifService.AddAsync(sinif);
                TempData["Bilgi"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(sinif);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _sinifService.GetAllAsync();
            var sinif = liste.FirstOrDefault(s => s.Id == id);

            if (sinif == null)
            {
                return NotFound();
            }

            return View(sinif);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Siniflar sinif)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _sinifService.UpdateAsync(sinif);
                TempData["Bilgi"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(sinif);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _sinifService.DeleteAsync(id);
            TempData["Bilgi"] = sonuc;
            return RedirectToAction(nameof(Index));
        }
    }
}
