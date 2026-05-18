using ApiLayer.ObsWeb.Services;
using isKatmani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    [Authorize(Policy = "ManagementViewer")]
    public class PersonelDetayController : Controller
    {
        private readonly PersonelDetayApiService _personelDetayService;

        public PersonelDetayController(PersonelDetayApiService personelDetayService)
        {
            _personelDetayService = personelDetayService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var detayliListe = await _personelDetayService.GetAllPersonelDetayAsync();
                return View(detayliListe);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Veriler yuklenirken bir hata olustu: " + ex.Message;
                return View(new List<PersonelDetayVM>());
            }
        }

        public async Task<IActionResult> Filtrele(string departmanAdi)
        {
            var liste = await _personelDetayService.GetAllPersonelDetayAsync();
            var filtrelenmis = liste.Where(p => p.DepartmanAd == departmanAdi).ToList();

            return View("Index", filtrelenmis);
        }
    }
}
