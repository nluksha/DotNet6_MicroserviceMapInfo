﻿using System.Text.Json;

namespace MicroserviceMapInfo.Services
{
    public interface IDistanceInfoSvc
    {
        Task<(int, string)> GetDistanceAsync(string originCity, string destinationCity);
    }

    public class DistanceInfoSvc : IDistanceInfoSvc
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DistanceInfoSvc(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Call the microservice to retrieve distance between two cities.
        /// </summary>
        /// <param name="originCity"></param>
        /// <param name="destinationCity"></param>
        /// <returns>Tuple for distance and the distance type.</returns>
        public async Task<(int, string)> GetDistanceAsync(string originCity, string destinationCity)
        {
            var httpClient = httpClientFactory.CreateClient("DistanceMicroservice");
            var microserviceUrl = $"?originCity={originCity}&destinationCity={destinationCity}";

            var responseStream = await httpClient.GetStreamAsync(microserviceUrl);
            var distanceData = await JsonSerializer.DeserializeAsync<MapDistanceInfo>(responseStream);

            var distance = 0;
            var distanceType = "";

            foreach (var row in distanceData.rows)
            {
                foreach (var rowElement in row.elements)
                {
                    if (int.TryParse(CleanDistanceInfo(rowElement.distance.text), out var distanceConverted))
                    {
                        distance += distanceConverted;
                        if (rowElement.distance.text.EndsWith("mi"))
                        {
                            distanceType = "miles";
                        }
                        else if (rowElement.distance.text.EndsWith("km"))
                        {
                            distanceType = "milometters";
                        }
                    }
                }
            }

            return (distance, distanceType);
        }

        private string CleanDistanceInfo(string value)
        {
            return value
                .Replace("mi", "")
                .Replace("km", "")
                .Replace(",", "");
        }
    }

    public class MapDistanceInfo
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }


}
