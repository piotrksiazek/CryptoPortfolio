using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExternalApiClientService : IExternalApiClientService
    {
        private HttpClient _apiClient;

        public ExternalApiClientService()
        {
            if (_apiClient == null)
            {
                _apiClient = new HttpClient();
                _apiClient.DefaultRequestHeaders.Accept.Clear();
                _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<string?> GetAsync(string url)
        {
            using (HttpResponseMessage response = await _apiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    if (json != null)
                    {
                        return json;
                    }

                }
                return null;
            }
        }
    }
}
