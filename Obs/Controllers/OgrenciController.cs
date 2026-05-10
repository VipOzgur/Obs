using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly OgrenciApiService _ogrenciService;

        public OgrenciController(OgrenciApiService ogrenciService)
        {
            _ogrenciService = ogrenciService;
        }

        // GET: Öğrenci Listeleme
        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _ogrenciService.GetAllAsync();
            return View(ogrenciler);
        }

        // GET: Yeni Öğrenci Ekleme Formu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yeni Öğrenci Kaydı
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

        // GET: Öğrenci Düzenleme Formu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _ogrenciService.GetAllAsync();
            var ogrenci = liste.FirstOrDefault(o => o.Id == id); // ID mülk adınızı kontrol edin

            if (ogrenci == null) return NotFound();

            return View(ogrenci);
        }

        // POST: Öğrenci Güncelleme
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

        // GET: Öğrenci Silme
        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _ogrenciService.DeleteAsync(id);
            TempData["BilgiMesaji"] = sonuc;
            return RedirectToAction(nameof(Index));
        }
    }
}