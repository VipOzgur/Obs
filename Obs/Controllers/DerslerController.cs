using ApiLayer;
using ApiLayer.ObsWeb.Services;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obs.Models;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class DerslerController : Controller
    {
        private readonly DerslerApiService _derslerService;
        private readonly PersonelApiService _personelService;
        private readonly SinifApiService _sinifService;

        public DerslerController(
            DerslerApiService derslerService,
            PersonelApiService personelService,
            SinifApiService sinifService)
        {
            _derslerService = derslerService;
            _personelService = personelService;
            _sinifService = sinifService;
        }

        public async Task<IActionResult> Index()
        {
            var dersler = await _derslerService.GetAllAsync();
            return View(dersler);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await BuildFormModelAsync(new Dersler()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DersFormViewModel model)
        {
            ValidateDers(model.Ders);

            if (!ModelState.IsValid)
            {
                return View(await BuildFormModelAsync(model.Ders));
            }

            TempData["Mesaj"] = await _derslerService.AddAsync(model.Ders);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ders = (await _derslerService.GetAllAsync()).FirstOrDefault(item => item.Id == id);

            if (ders == null)
            {
                return NotFound();
            }

            return View(await BuildFormModelAsync(ders));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DersFormViewModel model)
        {
            ValidateDers(model.Ders);

            if (!ModelState.IsValid)
            {
                return View(await BuildFormModelAsync(model.Ders));
            }

            TempData["Mesaj"] = await _derslerService.UpdateAsync(model.Ders);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Mesaj"] = await _derslerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<DersFormViewModel> BuildFormModelAsync(Dersler ders)
        {
            return new DersFormViewModel
            {
                Ders = ders,
                Personeller = await _personelService.GetAllAsync(),
                Siniflar = await _sinifService.GetAllAsync()
            };
        }

        private void ValidateDers(Dersler ders)
        {
            if (string.IsNullOrWhiteSpace(ders.ad))
            {
                ModelState.AddModelError("Ders.ad", "Ders adi zorunludur.");
            }

            if (ders.akts <= 0)
            {
                ModelState.AddModelError("Ders.akts", "AKTS 0'dan buyuk olmalidir.");
            }

            if (ders.donem <= 0)
            {
                ModelState.AddModelError("Ders.donem", "Donem 0'dan buyuk olmalidir.");
            }

            if (ders.personelId <= 0)
            {
                ModelState.AddModelError("Ders.personelId", "Ogretim uyesi secimi zorunludur.");
            }

            if (ders.sinifId <= 0)
            {
                ModelState.AddModelError("Ders.sinifId", "Sinif secimi zorunludur.");
            }
        }
    }
}
