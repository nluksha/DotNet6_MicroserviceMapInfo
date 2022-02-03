using GoogleMapInfo;

namespace MicroserviceMapInfo.Services
{
    public class DistanceInfoService: DistanceInfo.DistanceInfoBase
    {
        private readonly ILogger<DistanceInfoService> logger;
        private readonly GoogleDistanceApi googleDistanceApi;

        public DistanceInfoService(ILogger<DistanceInfoService> logger, GoogleDistanceApi googleDistanceApi)
        {
            this.logger = logger;
            this.googleDistanceApi = googleDistanceApi;
        }

        public override async Task<DistanceData> GetDistance(Cities cities, ServerCallContext context)
        {
            var totalMiles = "0";

            var distanceData = await googleDistanceApi.GetMapDistanceAsync(cities.OriginCity, cities.DestinationCity);

            foreach (var distanceDataRow in distanceData.rows)
            {
                foreach (var element in distanceDataRow.elements)
                {
                    totalMiles = element.distance.text;
                }
            }

            return new DistanceData { Miles = totalMiles };
        }
    }
}
