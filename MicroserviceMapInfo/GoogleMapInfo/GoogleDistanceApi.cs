using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoogleMapInfo
{
    public class GoogleDistanceApi
    {
        private readonly IConfiguration configuration;

        public GoogleDistanceApi(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GoogleDistanceData> GetMapDistanceAsync(string originCity, string destinationCity)
        {
            var apiKey = configuration["googleDistanceApi:apiKey"];
            var apiUrl = configuration["googleDistanceApi:apiUrl"];

            apiUrl += $"unit=imperial&origins={originCity}&destinations={destinationCity}&key={apiKey}";

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(apiUrl));

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            await using var data = await response.Content.ReadAsStreamAsync();
            var distanceInfo = await JsonSerializer.DeserializeAsync<GoogleDistanceData>(data);

            return distanceInfo;
        }
    }
}
