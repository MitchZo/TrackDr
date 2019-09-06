using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrackDr.Models;

namespace TrackDr.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrackDrDbContext _context;
        private readonly IConfiguration _configuration;
        public HomeController(TrackDrDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            string ApiKey = GetAPIKey();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public string GetAPIKey()
        {
            //Use this method to get the APIKey
            return _configuration.GetSection("AppConfiguration")["APIKeyValue"];
        }
        public async Task<IActionResult> Test()
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query=pediatrician&specialty_uid=pediatrician&skip=0&user_key={apiKey}");
            var test = await response.Content.ReadAsAsync<Rootobject>();
            //var test1 = await response.Content.ReadAsStringAsync(); <------Test condition
            return View(test);
            
        }
    }
}
