using ApiLayer;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class DepartmanIstatistikController : Controller
    {

        private readonly DepartmanIstatistikApiService _istatistikService;

        public DepartmanIstatistikController(DepartmanIstatistikApiService istatistikService)
        {
            _istatistikService = istatistikService;
        }

        // GET: Departman Istatistiklerini listeler
        public async Task<IActionResult> Index()
        {
            // Servis üzerinden API'ye istek atılıyor
            var istatistikler = await _istatistikService.GetDepartmanIstatistikleriAsync();

            // Veri gelmediyse kullanıcıya boş liste gönderilir
            return View(istatistikler);
        }

        // Dashboard gibi bir sayfada kısmi (partial) görünüm olarak kullanmak isterseniz:
        public async Task<IActionResult> GetStatsWidget()
        {
            var data = await _istatistikService.GetDepartmanIstatistikleriAsync();
            return PartialView("_IstatistikWidget", data);
        }
    }
}
