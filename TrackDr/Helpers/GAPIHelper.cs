using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackDr.Models;

namespace TrackDr.Helpers
{
    public class GAPIHelper : IGAPIHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseHelper _dbHelper;
        public GAPIHelper(IConfiguration configuration, IDatabaseHelper dbHelper)
        {
            _configuration = configuration;
            _dbHelper = dbHelper;
        }
        public string GetAPIKey()
        {
            //Use this method to get the APIKey
            return _configuration.GetSection("AppConfiguration")["GAPIKeyValue"];
        }
        //public string GooglefyUserAddress(string userName)
        //{
        //    AspNetUsers currentUser = _dbHelper.GetCurrentUser(userName);
        //    _dbHelper.GetCurrentParent(currentUser);
        //}
        //public string GooglefyString(string toBeGooglefied)
        //{

        //}
    }
}
