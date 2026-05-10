using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLayer;
using isKatmani;
namespace Obs.Controllers
{
    public class DepartmanController : Controller
    {
            private readonly DepartmanApiService _departmanService;

            // Constructor üzerinden servisi içeri alıyoruz (Dependency Injection)
            public DepartmanController(DepartmanApiService departmanService)
            {
                _departmanService = departmanService;
            }

            // GET: Departmanları listeler
            public async Task<IActionResult> Index()
            {
                var departmanlar = await _departmanService.GetAllAsync();
                return View(departmanlar);
            }

            // GET: Yeni Departman Sayfası
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // POST: Yeni Departman Kaydı
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Departmans departman)
            {
                if (ModelState.IsValid)
                {
                    var sonuc = await _departmanService.AddAsync(departman);
                    // API'den gelen mesajı TempData ile ekrana basabilirsiniz
                    TempData["Mesaj"] = sonuc;
                    return RedirectToAction(nameof(Index));
                }
                return View(departman);
            }

            // GET: Güncelleme Sayfası
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                // API'de GetById metodu olmadığı için tüm listeyi çekip filtreleyebiliriz 
                // (Ya da API servisinize GetByIdAsync eklemeniz daha performanslı olur)
                var liste = await _departmanService.GetAllAsync();
                var departman = liste.FirstOrDefault(x => x.Id == id); // ID kolon adınıza göre güncelleyin

                if (departman == null) return NotFound();

                return View(departman);
            }

            // POST: Güncelleme İşlemi
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Departmans departman)
            {
                if (ModelState.IsValid)
                {
                    var sonuc = await _departmanService.UpdateAsync(departman);
                    TempData["Mesaj"] = sonuc;
                    return RedirectToAction(nameof(Index));
                }
                return View(departman);
            }

            // GET: Silme Onay Sayfası (Veya doğrudan silme işlemi)
            public async Task<IActionResult> Delete(int id)
            {
                var sonuc = await _departmanService.DeleteAsync(id);
                TempData["Mesaj"] = sonuc;
                return RedirectToAction(nameof(Index));
            }
        
    }
}
