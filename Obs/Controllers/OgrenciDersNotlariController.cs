using ApiLayer;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class OgrenciDersNotlariController : Controller
    {
        private readonly OgrenciDersNotlariApiService _notApiService;

        public OgrenciDersNotlariController(OgrenciDersNotlariApiService notApiService)
        {
            _notApiService = notApiService;
        }

        // GET: OgrenciDersNotlari
        // Query string'den ogrenciId alabilir: /OgrenciDersNotlari/Index?ogrenciId=5
        public async Task<IActionResult> Index(int? ogrenciId)
        {
            // Servis içindeki metot ogrenciId parametresini alıp API'ye iletiyor
            var notlar = await _notApiService.GetNotlarAsync(ogrenciId);

            // Eğer belirli bir öğrenci için gelindiyse View tarafında başlığı özelleştirmek için
            ViewBag.OgrenciId = ogrenciId;

            return View(notlar);
        }

        // Not detaylarını gösteren alternatif bir action
        public async Task<IActionResult> Detay(int ogrenciId)
        {
            var ogrenciNotlari = await _notApiService.GetNotlarAsync(ogrenciId);

            if (ogrenciNotlari == null || !ogrenciNotlari.Any())
            {
                TempData["Hata"] = "Bu öğrenciye ait not kaydı bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(ogrenciNotlari);
        }
    }
}