using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
        public class FakulteApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/Fakulteler";

            public FakulteApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET: Tüm fakülteleri listeler
            public async Task<List<Fakulteler>> GetAllAsync()
            {
                var response = await _httpClient.GetFromJsonAsync<List<Fakulteler>>(BaseRoute);
                return response ?? new List<Fakulteler>();
            }

            // POST: Yeni bir fakülte ekler
            public async Task<string> AddAsync(Fakulteler fakulte)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, fakulte);
                return await response.Content.ReadAsStringAsync(); // "Eklendi" mesajını döner
            }

            // PUT: Mevcut fakülteyi günceller
            public async Task<string> UpdateAsync(Fakulteler fakulte)
            {
                var response = await _httpClient.PutAsJsonAsync(BaseRoute, fakulte);
                return await response.Content.ReadAsStringAsync(); // "Güncellendi" mesajını döner
            }

            // DELETE: Id üzerinden fakülte siler
            public async Task<string> DeleteAsync(int id)
            {
                // API'deki [HttpDelete("{id}")] kuralına uygun olarak: api/Fakulteler/5
                var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
                return await response.Content.ReadAsStringAsync(); // "Silindi" mesajını döner
            }
        }
    
}
