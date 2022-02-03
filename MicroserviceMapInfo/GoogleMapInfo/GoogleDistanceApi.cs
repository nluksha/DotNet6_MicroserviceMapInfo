using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // todo
        }
    }
}
