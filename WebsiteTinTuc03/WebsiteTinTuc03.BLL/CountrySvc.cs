using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.Req;
using Newtonsoft.Json;


namespace WebsiteTinTuc03.BLL
{
    public class CountrySvc
    {
        private readonly HttpClient _httpClient;

        public CountrySvc(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Country>> GetCountryInfo(string countryName)
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{countryName}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<Country>>(content);
                return countries;
            }
            else
            {
                throw new Exception("Không thể tải dữ liệu từ API.");
            }
        }




    }
}
