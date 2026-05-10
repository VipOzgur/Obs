using ApiLayer;
using isKatmani;
using Microsoft.AspNetCore.Mvc;

namespace Obs.Controllers
{
    public class FakulteController : Controller
    {
            private readonly FakulteApiService _fakulteApiService;

            public FakulteController(FakulteApiService fakulteApiService)
            {
                _fakulteApiService = fakulteApiService;
            }

            // GET: Fakülte Listesi
            public async Task<IActionResult> Index()
            {
                var fakulteler = await _fakulteApiService.GetAllAsync();
                return View(fakulteler);
            }

            // GET: Yeni Fakülte Ekleme Sayfası
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // POST: Yeni Fakülte Kaydet
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

            // GET: Fakülte Düzenleme Sayfası
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var liste = await _fakulteApiService.GetAllAsync();
                // ID mülkü modelinizde nasıl isimlendirildiyse (Id, FakulteId vb.) ona göre güncelleyin.
                var fakulte = liste.FirstOrDefault(f => f.Id == id);

                if (fakulte == null)
                {
                    return NotFound();
                }
                return View(fakulte);
            }

            // POST: Fakülte Güncelle
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

            // POST/GET: Fakülte Sil
            // Not: Güvenlik için silme işlemleri normalde [HttpPost] ile yapılır. 
            // Ancak hızlı test için basit bir linkle çalıştıracaksanız bu şekilde kullanabilirsiniz.
            public async Task<IActionResult> Delete(int id)
            {
                var mesaj = await _fakulteApiService.DeleteAsync(id);
                TempData["SuccessMessage"] = mesaj;
                return RedirectToAction(nameof(Index));
            }
        }
    }