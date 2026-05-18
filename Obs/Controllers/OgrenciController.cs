using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class OgrenciController : Controller
    {
        private readonly OgrenciApiService _ogrenciService;

        public OgrenciController(OgrenciApiService ogrenciService)
        {
            _ogrenciService = ogrenciService;
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _ogrenciService.GetAllAsync();
            return View(ogrenciler);
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
        public async Task<IActionResult> Create(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _ogrenciService.AddAsync(ogrenci);
                TempData["BilgiMesaji"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(ogrenci);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _ogrenciService.GetAllAsync();
            var ogrenci = liste.FirstOrDefault(o => o.Id == id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await _ogrenciService.UpdateAsync(ogrenci);
                TempData["BilgiMesaji"] = sonuc;
                return RedirectToAction(nameof(Index));
            }

            return View(ogrenci);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _ogrenciService.DeleteAsync(id);
            TempData["BilgiMesaji"] = sonuc;
            return RedirectToAction(nameof(Index));
        }
    }
}
