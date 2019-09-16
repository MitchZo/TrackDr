using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackDr.Helpers
{
    public interface IGAPIHelper
    {
        string GetAPIKey();
        string GooglefyUserAddress(string userName);
        string GooglefyString(string toBeGooglefied);
        Task<string> GetTravelInfo(string startAddress, string endAddress);
    }
}
