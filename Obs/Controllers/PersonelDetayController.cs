using ApiLayer.ObsWeb.Services;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class PersonelDetayController : Controller
    {
            private readonly PersonelDetayApiService _personelDetayService;

            public PersonelDetayController(PersonelDetayApiService personelDetayService)
            {
                _personelDetayService = personelDetayService;
            }

            // GET: PersonelDetay
            // Tüm personellerin detaylı listesini tablo olarak döndürür
            public async Task<IActionResult> Index()
            {
                try
                {
                    var detayliListe = await _personelDetayService.GetAllPersonelDetayAsync();
                    return View(detayliListe);
                }
                catch (Exception ex)
                {
                    // Hata durumunda boş liste gönderip kullanıcıya bilgi verebilirsiniz
                    ViewBag.Error = "Veriler yüklenirken bir hata oluştu: " + ex.Message;
                    return View(new List<PersonelDetayVM>());
                }
            }

            // İsterseniz belirli bir departmana göre filtreleme yapan bir Action da ekleyebilirsiniz
            public async Task<IActionResult> Filtrele(string departmanAdi)
            {
                var liste = await _personelDetayService.GetAllPersonelDetayAsync();
                var filtrelenmiş = liste.Where(p => p.DepartmanAd == departmanAdi).ToList();

                return View("Index", filtrelenmiş);
            }
        }
    }