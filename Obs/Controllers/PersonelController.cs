using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class PersonelController : Controller
    {
            private readonly PersonelApiService _personelService;
            private readonly DepartmanApiService _departmanService; // Opsiyonel: Departman seçimi için

            public PersonelController(PersonelApiService personelService, DepartmanApiService departmanService)
            {
                _personelService = personelService;
                _departmanService = departmanService;
            }

            // GET: Tüm Personelleri Listele
            public async Task<IActionResult> Index()
            {
                var personeller = await _personelService.GetAllAsync();
                return View(personeller);
            }

            // GET: Yeni Personel Ekleme Sayfası
            [HttpGet]
            public async Task<IActionResult> Create()
            {
                // Personel eklerken departman seçtirmek isterseniz:
                // ViewBag.Departmanlar = await _departmanService.GetAllAsync();
                return View();
            }

            // POST: Yeni Personel Kaydet
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Personel personel)
            {
                if (ModelState.IsValid)
                {
                    var mesaj = await _personelService.AddAsync(personel);
                    TempData["Durum"] = mesaj;
                    return RedirectToAction(nameof(Index));
                }
                return View(personel);
            }

            // GET: Personel Güncelleme Sayfası
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var liste = await _personelService.GetAllAsync();
                var personel = liste.FirstOrDefault(p => p.Id == id); // ID kolon adınıza göre kontrol edin

                if (personel == null)
                {
                    return NotFound();
                }

                // ViewBag.Departmanlar = await _departmanService.GetAllAsync();
                return View(personel);
            }

            // POST: Personel Güncelle
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Personel personel)
            {
                if (ModelState.IsValid)
                {
                    var mesaj = await _personelService.UpdateAsync(personel);
                    TempData["Durum"] = mesaj;
                    return RedirectToAction(nameof(Index));
                }
                return View(personel);
            }

            // GET: Personel Sil
            public async Task<IActionResult> Delete(int id)
            {
                var mesaj = await _personelService.DeleteAsync(id);
                TempData["Durum"] = mesaj;
                return RedirectToAction(nameof(Index));
            }
        }
    }