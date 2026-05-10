using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class RolesController : Controller
    {
            private readonly RolesApiService _rolesService;

            public RolesController(RolesApiService rolesService)
            {
                _rolesService = rolesService;
            }

            // GET: Rollerin Listesi
            public async Task<IActionResult> Index()
            {
                var roller = await _rolesService.GetAllAsync();
                return View(roller);
            }

            // GET: Yeni Rol Ekleme Sayfası
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // POST: Yeni Rol Kaydet
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

            // GET: Rol Düzenleme
            // Not: Servisiniz "Güncelleme yok" döneceği için bu sayfa sadece bilgi amaçlıdır
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var liste = await _rolesService.GetAllAsync();
                var role = liste.FirstOrDefault(r => r.Id == id); // ID mülkünüzü kontrol edin

                if (role == null) return NotFound();

                return View(role);
            }

            // POST: Rol Güncelle
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Roles role)
            {
                var sonuc = await _rolesService.UpdateAsync(role);

                // API'den "Rolede Güncelleme yok" mesajı gelirse kullanıcıya gösteriyoruz
                TempData["Mesaj"] = sonuc;

                return RedirectToAction(nameof(Index));
            }

            // GET: Rol Sil
            public async Task<IActionResult> Delete(int id)
            {
                var sonuc = await _rolesService.DeleteAsync(id);
                TempData["Mesaj"] = sonuc;
                return RedirectToAction(nameof(Index));
            }
        }
    }