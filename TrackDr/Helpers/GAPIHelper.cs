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

        public string GooglefyUserAddress(string userName)
        {
            string returnString = "";
            AspNetUsers currentUser = _dbHelper.GetCurrentUser(userName);
            Parent currentParent = _dbHelper.GetCurrentParent(currentUser);

            returnString += GooglefyString(currentParent.HouseNumber + "+");
            returnString += GooglefyString(currentParent.Street + "+");

            if(currentParent.Street2 != null)
            { returnString += GooglefyString(currentParent.Street2) + "+"; }

            returnString += GooglefyString(currentParent.City) + "+";
            returnString += GooglefyString(currentParent.State);

            return returnString;
        }
        public string GooglefyString(string toBeGooglefied)
        {
            string returnString = "";
            string[] words = toBeGooglefied.Split(' ');
            if (words.Length > 0)
            {
                foreach (string word in words)
                {
                    returnString += word;
                }
            }
            else
            {
                return toBeGooglefied;
            }
            return returnString;
        }
        public void DetermineDistance(string startAddress, string endAddress)
        {
        }

    }
}
