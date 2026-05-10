using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
    using System.Net.Http.Json;
    using isKatmani; // Siniflar modelinin bulunduğu namespace

    namespace ObsWeb.Services
    {
        public class SinifApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/Siniflar";

            public SinifApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET: Tüm sınıfları listeler
            public async Task<List<Siniflar>> GetAllAsync()
            {
                var response = await _httpClient.GetFromJsonAsync<List<Siniflar>>(BaseRoute);
                return response ?? new List<Siniflar>();
            }

            // POST: Yeni bir sınıf ekler
            public async Task<string> AddAsync(Siniflar sinif)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, sinif);
                return await response.Content.ReadAsStringAsync(); // "Eklendi" döner
            }

            // PUT: Mevcut sınıf bilgilerini günceller
            public async Task<string> UpdateAsync(Siniflar sinif)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, sinif);
                return await response.Content.ReadAsStringAsync(); // "Güncellendi" döner
            }

            // DELETE: ID'ye göre sınıf siler
            public async Task<string> DeleteAsync(int id)
            {
                // API Rotası: api/Siniflar/5
                var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
                return await response.Content.ReadAsStringAsync(); // "Silindi" döner
            }
        }
    }
}
