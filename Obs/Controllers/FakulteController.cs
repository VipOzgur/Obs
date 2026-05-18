using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class FakulteController : Controller
    {
        private readonly FakulteApiService _fakulteApiService;

        public FakulteController(FakulteApiService fakulteApiService)
        {
            _fakulteApiService = fakulteApiService;
        }

        public async Task<IActionResult> Index()
        {
            var fakulteler = await _fakulteApiService.GetAllAsync();
            return View(fakulteler);
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
        public async Task<IActionResult> Create(Fakulteler fakulte)
        {
            if (ModelState.IsValid)
            {
                var mesaj = await _fakulteApiService.AddAsync(fakulte);
                TempData["SuccessMessage"] = mesaj;
                return RedirectToAction(nameof(Index));
            }

            return View(fakulte);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var liste = await _fakulteApiService.GetAllAsync();
            var fakulte = liste.FirstOrDefault(f => f.Id == id);

            if (fakulte == null)
            {
                return NotFound();
            }

            return View(fakulte);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Fakulteler fakulte)
        {
            if (ModelState.IsValid)
            {
                var mesaj = await _fakulteApiService.UpdateAsync(fakulte);
                TempData["SuccessMessage"] = mesaj;
                return RedirectToAction(nameof(Index));
            }

            return View(fakulte);
        }

        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var mesaj = await _fakulteApiService.DeleteAsync(id);
            TempData["SuccessMessage"] = mesaj;
            return RedirectToAction(nameof(Index));
        }
    }
}
