using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
    
        public class OgrenciApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/Ogrenci";

            public OgrenciApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET: Tüm öğrencileri getirir
            public async Task<List<OgrenciSP>> GetAllAsync()
            {
                var response = await _httpClient.GetFromJsonAsync<List<OgrenciSP>>(BaseRoute);
                return response ?? new List<OgrenciSP>();
            }

            // POST: Yeni öğrenci ekler
            public async Task<string> AddAsync(OgrenciSP ogrenci)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, ogrenci);
                return await response.Content.ReadAsStringAsync(); // "Eklendi"
            }

            // PUT: Öğrenci bilgilerini günceller
            public async Task<string> UpdateAsync(OgrenciSP ogrenci)
            {
                var response = await _httpClient.PutAsJsonAsync(BaseRoute, ogrenci);
                return await response.Content.ReadAsStringAsync(); // "Güncellendi"
            }

            // DELETE: ID'ye göre öğrenci siler
            public async Task<string> DeleteAsync(int id)
            {
                // Endpoint: api/Ogrenci/{id}
                var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
                return await response.Content.ReadAsStringAsync(); // "Silindi"
            }
        
    }
}
