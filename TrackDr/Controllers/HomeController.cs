using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
            return _configuration.GetSection("AppConfiguration")["BDAPIKeyValue"];
        }
        //public async Task<IActionResult> SelectedDoctorUid()
        //{
        //    string apiKey = GetAPIKey();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.betterdoctor.com");
        //    var response = await client.GetAsync($"/2016-03-01/doctors?query=pediatrician&specialty_uid=pediatrician&skip=0&user_key={apiKey}");
        //    var test = await response.Content.ReadAsAsync<Rootobject>();
        //    //var test1 = await response.Content.ReadAsStringAsync(); <------Test condition
        //    AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
        //    //we are returning the first doctor in the api's UID
        //    //AddToDb(test.data[0].uid);
        //    string uid = test.data[6].uid;
        //    Doctor newDoctor = new Doctor();
        //    newDoctor.Id = uid;
        //    _context.Doctor.Add(newDoctor);
        //    _context.SaveChanges();
        //    return View("ListDoctor", newDoctor);
            
        //}
        
        public IActionResult AddDoctor(Datum doctor)
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query=pediatrician&specialty_uid=pediatrician&skip=0&user_key={apiKey}");
            var test = await response.Content.ReadAsAsync<Rootobject>();
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            string uid = test.data[6].uid;
            Doctor newDoctor = new Doctor();
            newDoctor.Id = uid;
            _context.Doctor.Add(newDoctor);
            _context.SaveChanges();
            return View("ListDoctor", newDoctor);
        }

        public IActionResult ListDoctor()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string userInput)
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query={userInput}&specialty_uid=pediatrician&user_key={apiKey}");
            var result = await response.Content.ReadAsAsync<Rootobject>();
           
            return View("ListDoctors", result);
        }
        public IActionResult AddDoctor(Datum doctor)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            UserDoctor savedDoctors = new UserDoctor();
            if (ModelState.IsValid)
            {
                Doctor newDoctor = new Doctor();
                newDoctor.Id = doctor.uid;
                _context.Doctor.Add(newDoctor);
                _context.SaveChanges();

             
                savedDoctors.UserId = thisUser.Id;
                savedDoctors.DoctorId = doctor.uid;

                _context.UserDoctor.Add(savedDoctors);
                _context.SaveChanges();
                return View("ListDoctors");
            }
            return View("ListDoctors");
        }
        public IActionResult Add()
        {
            return View();
        }

        // saves the user's address to the UserDb as well as the user's Id number and their ASP Id
        [HttpPost]
        public IActionResult RegisterUser(User newUserInfo)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            User newUser = new User();
            newUser.HouseNumber = newUserInfo.HouseNumber;
            newUser.Street = newUserInfo.Street;
            newUser.Street2 = newUserInfo.Street2;
            newUser.City = newUserInfo.City;
            newUser.State = newUserInfo.State;
            newUser.ZipCode = newUserInfo.ZipCode;
            newUser.UserId = thisUser.Id;
            newUser.Id = newUserInfo.Id;

            _context.User.Add(newUser);
            _context.SaveChanges();

            return View("Search");
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult UserInformaton()
        {
            return View();
        }


    }
}
