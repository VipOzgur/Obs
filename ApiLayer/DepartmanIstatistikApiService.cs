using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using isKatmani;
namespace ApiLayer
{
    public class DepartmanIstatistikApiService
    {
        private readonly HttpClient _httpClient;

        public DepartmanIstatistikApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DepartmanIstatistikVMSP>> GetDepartmanIstatistikleriAsync()
        {
            // Controller'daki [Route("api/[controller]")] tanımına göre url: api/DepartmanIstatistik
            var response = await _httpClient.GetAsync("api/DepartmanIstatistik");

            if (response.IsSuccessStatusCode)
            {
                // Veriyi otomatik olarak Listeye deserialize eder
                return await response.Content.ReadFromJsonAsync<List<DepartmanIstatistikVMSP>>();
            }

            return new List<DepartmanIstatistikVMSP>();
        }
    }
}
