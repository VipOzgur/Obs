using isKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
    public class OgrenciDersNotlariApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseRoute = "api/OgrenciDersNotlari";

        public OgrenciDersNotlariApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Öğrencinin ders notlarını getirir. 
        /// ogrenciId null gönderilirse tüm notlar gelebilir (API logic'ine bağlı).
        /// </summary>
        public async Task<List<OgrenciDersNotlariVMSP>> GetNotlarAsync(int? ogrenciId)
        {
            // Endpoint yapısı: api/OgrenciDersNotlari/notlar?ogrenciId=5
            string url = $"{BaseRoute}/notlar";

            if (ogrenciId.HasValue)
            {
                url += $"?ogrenciId={ogrenciId.Value}";
            }

            var response = await _httpClient.GetFromJsonAsync<List<OgrenciDersNotlariVMSP>>(url);
            return response ?? new List<OgrenciDersNotlariVMSP>();
        }
    }

}
