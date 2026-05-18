using ApiLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class DepartmanIstatistikController : Controller
    {
        private readonly DepartmanIstatistikApiService _istatistikService;

        public DepartmanIstatistikController(DepartmanIstatistikApiService istatistikService)
        {
            _istatistikService = istatistikService;
        }

        public async Task<IActionResult> Index()
        {
            var istatistikler = await _istatistikService.GetDepartmanIstatistikleriAsync();
            return View(istatistikler);
        }

        public async Task<IActionResult> GetStatsWidget()
        {
            var data = await _istatistikService.GetDepartmanIstatistikleriAsync();
            return PartialView("_IstatistikWidget", data);
        }
    }
}
