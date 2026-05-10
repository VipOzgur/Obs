using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
    public class DepartmanApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseRoute = "api/Departmans";

        public DepartmanApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Tüm listeyi getirir
        public async Task<List<DepartmansSP>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmansSP>>(BaseRoute)
                   ?? new List<DepartmansSP>();
        }

        // POST: Yeni departman ekler
        public async Task<string> AddAsync(DepartmansSP departman)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, departman);
            return await response.Content.ReadAsStringAsync(); // "Eklendi" döner
        }

        // PUT: Departman günceller
        public async Task<string> UpdateAsync(DepartmansSP departman)
        {
            var response = await _httpClient.PutAsJsonAsync(BaseRoute, departman);
            return await response.Content.ReadAsStringAsync(); // "Güncellendi" döner
        }

        // DELETE: Id'ye göre departman siler
        public async Task<string> DeleteAsync(int id)
        {
            // Delete metodunda ID route üzerinden gönderilir: api/Departmans/5
            var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
            return await response.Content.ReadAsStringAsync(); // "Silindi" döner
        }
    }
}
