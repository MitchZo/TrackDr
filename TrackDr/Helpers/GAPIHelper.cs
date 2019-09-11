using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackDr.Helpers
{
    public class GAPIHelper : IGAPIHelper
    {
        private readonly IConfiguration _configuration;
        public GAPIHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetAPIKey()
        {
            //Use this method to get the APIKey
            return _configuration.GetSection("AppConfiguration")["GAPIKeyValue"];
        }
    }
}
