using System.Security.Claims;
using ApiLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "GradesViewer")]
    public class OgrenciDersNotlariController : Controller
    {
        private readonly OgrenciDersNotlariApiService _notApiService;

        public OgrenciDersNotlariController(OgrenciDersNotlariApiService notApiService)
        {
            _notApiService = notApiService;
        }

        public async Task<IActionResult> Index(int? ogrenciId)
        {
            if (IsLimitedGradeUser())
            {
                ogrenciId = GetCurrentUserId();
            }

            var notlar = await _notApiService.GetNotlarAsync(ogrenciId);
            ViewBag.OgrenciId = ogrenciId;
            ViewBag.CanFilterGrades = !IsLimitedGradeUser();

            return View(notlar);
        }

        public async Task<IActionResult> Detay(int ogrenciId)
        {
            if (IsLimitedGradeUser() && ogrenciId != GetCurrentUserId())
            {
                return Forbid();
            }

            var ogrenciNotlari = await _notApiService.GetNotlarAsync(ogrenciId);

            if (ogrenciNotlari == null || !ogrenciNotlari.Any())
            {
                TempData["Hata"] = "Bu ogrenciye ait not kaydi bulunamadi.";
                return RedirectToAction(nameof(Index));
            }

            return View(ogrenciNotlari);
        }

        private bool IsLimitedGradeUser()
        {
            return User.IsInRole("User") || User.IsInRole("Ogrenci");
        }

        private int GetCurrentUserId()
        {
            var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claimValue, out var id) ? id : 0;
        }
    }
}
