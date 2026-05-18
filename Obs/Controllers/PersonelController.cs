using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class PersonelController : Controller
    {
        private readonly PersonelApiService _personelService;

        public PersonelController(PersonelApiService personelService, DepartmanApiService departmanService)
        {
            _personelService = personelService;
        }

        public async Task<IActionResult> Index()
        {
            var personeller = await _personelService.GetAllAsync();
            return View(personeller);
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

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _personelService.GetAllAsync();
            var personel = liste.FirstOrDefault(p => p.Id == id);

            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        [Authorize(Policy = "SuperAdminOnly")]
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

        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var mesaj = await _personelService.DeleteAsync(id);
            TempData["Durum"] = mesaj;
            return RedirectToAction(nameof(Index));
        }
    }
}
