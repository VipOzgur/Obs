using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer
{
    using System.Net.Http.Json;
    using isKatmani; // PersonelDetayViewModel (veya ilgili model) için

    namespace ObsWeb.Services
    {
        public class PersonelDetayApiService
        {
            private readonly HttpClient _httpClient;
            private const string BaseRoute = "api/PersonelDetay";

            public PersonelDetayApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            /// <summary>
            /// Personel bilgilerini detaylı (muhtemelen Join işlemlerinden geçmiş) olarak getirir.
            /// </summary>
            public async Task<List<PersonelDetayVMSP>> GetAllPersonelDetayAsync()
            {
                // Controller'daki [HttpGet] metoduna istek atar
                var response = await _httpClient.GetFromJsonAsync<List<PersonelDetayVMSP>>(BaseRoute);

                return response ?? new List<PersonelDetayVMSP>();
            }
        }
    }
}
