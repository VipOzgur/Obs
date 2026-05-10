using ApiLayer.ObsWeb.Services;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class SinifController : Controller
    {
            private readonly SinifApiService _sinifService;

            public SinifController(SinifApiService sinifService)
            {
                _sinifService = sinifService;
            }

            // GET: Tüm Sınıfları Listeler
            public async Task<IActionResult> Index()
            {
                var siniflar = await _sinifService.GetAllAsync();
                return View(siniflar);
            }

            // GET: Sınıf Ekleme Sayfası
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // POST: Sınıf Ekleme
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

            // GET: Sınıf Düzenleme Sayfası
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var liste = await _sinifService.GetAllAsync();
                var sinif = liste.FirstOrDefault(s => s.Id == id); // ID mülk adını kontrol edin

                if (sinif == null) return NotFound();

                return View(sinif);
            }

            // POST: Sınıf Güncelleme
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

            // GET: Sınıf Silme İşlemi
            public async Task<IActionResult> Delete(int id)
            {
                var sonuc = await _sinifService.DeleteAsync(id);
                TempData["Bilgi"] = sonuc;
                return RedirectToAction(nameof(Index));
            }
        }
    }