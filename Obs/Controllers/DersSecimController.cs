using System.Security.Claims;
using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obs.Models;

namespace Obs.Controllers
{
    [Authorize(Policy = "GradesViewer")]
    public class DersSecimController : Controller
    {
        private readonly OgrDersApiService _ogrDersService;
        private readonly DerslerApiService _derslerService;
        private readonly OgrenciApiService _ogrenciService;

        public DersSecimController(
            OgrDersApiService ogrDersService,
            DerslerApiService derslerService,
            OgrenciApiService ogrenciService)
        {
            _ogrDersService = ogrDersService;
            _derslerService = derslerService;
            _ogrenciService = ogrenciService;
        }

        public async Task<IActionResult> Index(int? ogrenciId)
        {
            if (IsLimitedUser())
            {
                ogrenciId = GetCurrentUserId();
            }

            List<Ogr_Ders> secimler;
            try
            {
                secimler = await _ogrDersService.GetAllAsync();
            }
            catch
            {
                ViewBag.Error = "Ders secimleri yuklenirken API tarafinda bir hata olustu.";
                secimler = new List<Ogr_Ders>();
            }

            if (ogrenciId.HasValue)
            {
                secimler = secimler.Where(item => item.ogrId == ogrenciId.Value).ToList();
            }

            var model = new DersSecimViewModel
            {
                OgrenciId = ogrenciId,
                Secimler = secimler,
                Dersler = await SafeGetDerslerAsync(),
                Ogrenciler = IsLimitedUser() ? new List<Ogrenci>() : await SafeGetOgrencilerAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? ogrenciId)
        {
            var currentUserId = GetCurrentUserId();

            var model = new DersSecimFormViewModel
            {
                OgrenciId = IsLimitedUser() ? currentUserId : ogrenciId ?? 0,
                Dersler = await SafeGetDerslerAsync(),
                Ogrenciler = IsLimitedUser() ? new List<Ogrenci>() : await SafeGetOgrencilerAsync(),
                OnayDurumu = IsLimitedUser() ? "Beklemede" : "Onaylandi"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DersSecimFormViewModel model)
        {
            if (IsLimitedUser())
            {
                model.OgrenciId = GetCurrentUserId();
                model.OnayDurumu = "Beklemede";
                model.Puan = 0;
                model.Basari = false;
            }

            if (model.OgrenciId <= 0)
            {
                ModelState.AddModelError(nameof(model.OgrenciId), "Ogrenci secimi zorunludur.");
            }

            if (await IsDuplicateSelectionAsync(model.OgrenciId, model.DersId, model.Id))
            {
                ModelState.AddModelError(string.Empty, "Bu ders secimi zaten kayitli.");
            }

            if (!ModelState.IsValid)
            {
                await FillFormListsAsync(model);
                return View(model);
            }

            var secim = new Ogr_Ders
            {
                ogrId = model.OgrenciId,
                dersId = model.DersId,
                puan = model.Puan,
                basari = model.Basari,
                onayDurumu = NormalizeOnayDurumu(model.OnayDurumu)
            };

            TempData["Mesaj"] = await _ogrDersService.AddAsync(secim);
            int? redirectOgrenciId = IsLimitedUser() ? null : model.OgrenciId;
            return RedirectToAction(nameof(Index), new { ogrenciId = redirectOgrenciId });
        }

        [Authorize(Policy = "ManagementViewer")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var secim = (await _ogrDersService.GetAllAsync()).FirstOrDefault(item => item.Id == id);

            if (secim == null)
            {
                return NotFound();
            }

            var model = new DersSecimFormViewModel
            {
                Id = secim.Id,
                OgrenciId = secim.ogrId,
                DersId = secim.dersId,
                Puan = secim.puan,
                Basari = secim.basari,
                OnayDurumu = secim.onayDurumu
            };

            await FillFormListsAsync(model);
            return View(model);
        }

        [Authorize(Policy = "ManagementViewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DersSecimFormViewModel model)
        {
            if (model.OgrenciId <= 0)
            {
                ModelState.AddModelError(nameof(model.OgrenciId), "Ogrenci secimi zorunludur.");
            }

            if (await IsDuplicateSelectionAsync(model.OgrenciId, model.DersId, model.Id))
            {
                ModelState.AddModelError(string.Empty, "Bu ogrenci icin ayni ders zaten kayitli.");
            }

            if (!ModelState.IsValid)
            {
                await FillFormListsAsync(model);
                return View(model);
            }

            var secim = new Ogr_Ders
            {
                Id = model.Id,
                ogrId = model.OgrenciId,
                dersId = model.DersId,
                puan = model.Puan,
                basari = model.Basari,
                onayDurumu = NormalizeOnayDurumu(model.OnayDurumu)
            };

            TempData["Mesaj"] = await _ogrDersService.UpdateAsync(secim);
            return RedirectToAction(nameof(Index), new { ogrenciId = model.OgrenciId });
        }

        [Authorize(Policy = "ManagementViewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            TempData["Mesaj"] = await _ogrDersService.ApproveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ManagementViewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            TempData["Mesaj"] = await _ogrDersService.RejectAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ManagementViewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Mesaj"] = await _ogrDersService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillFormListsAsync(DersSecimFormViewModel model)
        {
            model.Dersler = await SafeGetDerslerAsync();
            model.Ogrenciler = IsLimitedUser() ? new List<Ogrenci>() : await SafeGetOgrencilerAsync();
        }

        private async Task<bool> IsDuplicateSelectionAsync(int ogrenciId, int dersId, int currentId)
        {
            var secimler = await _ogrDersService.GetAllAsync();
            return secimler.Any(item =>
                item.Id != currentId &&
                item.ogrId == ogrenciId &&
                item.dersId == dersId &&
                item.onayDurumu != "Reddedildi");
        }

        private bool IsLimitedUser()
        {
            return User.IsInRole("User") || User.IsInRole("Ogrenci");
        }

        private int GetCurrentUserId()
        {
            var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claimValue, out var id) ? id : 0;
        }

        private static string NormalizeOnayDurumu(string? value)
        {
            return value switch
            {
                "Onaylandi" => "Onaylandi",
                "Reddedildi" => "Reddedildi",
                _ => "Beklemede"
            };
        }

        private async Task<List<Dersler>> SafeGetDerslerAsync()
        {
            try
            {
                return await _derslerService.GetAllAsync();
            }
            catch
            {
                return new List<Dersler>();
            }
        }

        private async Task<List<Ogrenci>> SafeGetOgrencilerAsync()
        {
            try
            {
                return await _ogrenciService.GetAllAsync();
            }
            catch
            {
                return new List<Ogrenci>();
            }
        }
    }
}
