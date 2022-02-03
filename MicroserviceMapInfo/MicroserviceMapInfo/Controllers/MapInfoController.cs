using Microsoft.AspNetCore.Mvc;
using GoogleMapInfo;

namespace MicroserviceMapInfo.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class MapInfoController : ControllerBase
    {
        private readonly GoogleDistanceApi googleDistanceApi;

        public MapInfoController(GoogleDistanceApi googleDistanceApi)
        {
            this.googleDistanceApi = googleDistanceApi;
        }

        [HttpGet]
        public async Task<GoogleDistanceData> GetDistance(string originCity, string destinationCity)
        {
            return await googleDistanceApi.GetMapDistanceAsync(originCity, destinationCity);
        }
    }
}
