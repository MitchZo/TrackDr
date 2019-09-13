using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrackDr.Models;

namespace TrackDr.Helpers
{
    public class BDAPIHelper : IBDAPIHelper
    {

        private readonly IConfiguration _configuration;
        public BDAPIHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<SingleDoctor> GetDoctor(string doctorId)
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors/{doctorId}?user_key={apiKey}");
            var result = await response.Content.ReadAsAsync<SingleDoctor>();

            return result;
        }
        public string GetAPIKey()
        {
            //Use this method to get the APIKey
            return _configuration.GetSection("AppConfiguration")["BDAPIKeyValue"];
        }
        //public async Task<Rootobject> GetDoctorList(string userInput, string userState, string userInsurance)
        //{
        //    string apiKey = GetAPIKey();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.betterdoctor.com");
        //    var response = await client.GetAsync($"/2016-03-01/doctors?query={userInput}&specialty_uid=pediatrician&limit=100&user_key={apiKey}"); //EDITED THIS 
        //    return await response.Content.ReadAsAsync<Rootobject>();
        //}

        //public async Task<Rootobject> GetDoctorList(string userInput)
        //{
        //    string apiKey = GetAPIKey();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.betterdoctor.com");
        //    var response = await client.GetAsync($"/2016-03-01/doctors?query={userInput}&specialty_uid=pediatrician&limit=100&user_key={apiKey}"); //EDITED THIS 
        //    return await response.Content.ReadAsAsync<Rootobject>();
        //}
        public async Task<Rootobject> GetDoctorList(string userInput, string userState, string userInsurance)
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query={userInput}&specialty_uid=pediatrician&limit=100&location={userState}&insurance_uid={userInsurance}&user_key={apiKey}"); //EDITED THIS 
            return await response.Content.ReadAsAsync<Rootobject>();
        }

        //public async Task<Rootobject> GetDoctorListByInsurance(string userInput, string userState, string userInsurance)
        //{
        //    string apiKey = GetAPIKey();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.betterdoctor.com");
        //    var response = await client.GetAsync($"/2016-03-01/doctors?query={userInput}&specialty_uid=pediatrician&limit=100&location={userState}&insurance_uid={userInsurance}&user_key={apiKey}"); //EDITED THIS 
        //    return await response.Content.ReadAsAsync<Rootobject>();

        //}
    }
}
