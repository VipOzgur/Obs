using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
        public class PersonelApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/Personel";

            public PersonelApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET: Tüm personel listesini getirir
            public async Task<List<Personel>> GetAllAsync()
            {
                var response = await _httpClient.GetFromJsonAsync<List<Personel>>(BaseRoute);
                return response ?? new List<Personel>();
            }

            // POST: Yeni personel ekler
            public async Task<string> AddAsync(Personel personel)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, personel);
                return await response.Content.ReadAsStringAsync(); // API'den gelen "Eklendi" mesajı
            }

            // PUT: Personel bilgilerini günceller
            public async Task<string> UpdateAsync(Personel personel)
            {
                var response = await _httpClient.PutAsJsonAsync(BaseRoute, personel);
                return await response.Content.ReadAsStringAsync(); // API'den gelen "Güncellendi" mesajı
            }

            // DELETE: ID parametresi ile personel siler
            public async Task<string> DeleteAsync(int id)
            {
                // API Rotası: api/Personel/{id}
                var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
                return await response.Content.ReadAsStringAsync(); // API'den gelen "Silindi" mesajı
            }
        
    }
}
