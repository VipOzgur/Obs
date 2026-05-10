using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
        public class RolesApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/Roles";

            public RolesApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET: Tüm rolleri listeler
            public async Task<List<Roles>> GetAllAsync()
            {
                var response = await _httpClient.GetFromJsonAsync<List<Roles>>(BaseRoute);
                return response ?? new List<Roles>();
            }

            // POST: Yeni rol ekler
            public async Task<string> AddAsync(Roles role)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseRoute, role);
                return await response.Content.ReadAsStringAsync(); // "Eklendi"
            }

            // PUT: Rol günceller 
            // Not: Controller tarafında "Güncelleme yok" mesajı döndüğü için bu metod o mesajı iletir.
            public async Task<string> UpdateAsync(Roles role)
            {
                var response = await _httpClient.PutAsJsonAsync(BaseRoute, role);
                return await response.Content.ReadAsStringAsync(); // "Rolede Güncelleme yok"
            }

            // DELETE: ID üzerinden rol siler
            public async Task<string> DeleteAsync(int id)
            {
                // API: api/Roles/5
                var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
                return await response.Content.ReadAsStringAsync(); // "Silindi"
            }
        }
    
}
