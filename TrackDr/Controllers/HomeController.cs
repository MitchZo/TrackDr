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
            return _configuration.GetSection("AppConfiguration")["BDAPIKeyValue"];
        }
        public async Task<IActionResult> SelectedDoctorUid()
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query=pediatrician&specialty_uid=pediatrician&skip=0&user_key={apiKey}");
            var test = await response.Content.ReadAsAsync<Rootobject>();
            //var test1 = await response.Content.ReadAsStringAsync(); <------Test condition
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            //we are returning the first doctor in the api's UID
            //AddToDb(test.data[0].uid);
            string uid = test.data[6].uid;
            DoctorUid newDoctor = new DoctorUid();
            newDoctor.Id = uid;
            _context.DoctorUid.Add(newDoctor);
            _context.SaveChanges();
            return View("ListDoctor", newDoctor);
            
        }

        // next we want to add this UID to the DoctorUid Database
        // we won't be returning anything from this method
        //public IActionResult AddToDb()
        //{
        //    // this sets doctorUid to the UID we got from the API in SelectedDoctorUid()
        //    string doctorUid = SelectedDoctorUid().ToString();
        //    // save UId to DoctorUid.Id
        //    DoctorUid newDoctor = new DoctorUid();
        //    newDoctor.Id = doctorUid;
        //    _context.DoctorUid.Add(newDoctor);
        //    _context.SaveChanges();
        //    // in theory, this will store our Doctor UID in DoctorUid MODEL that we got from our API
        //    // which is stored in a new doctor object in the datatabase 
        //    return RedirectToAction("Test");

        //}

        public IActionResult ListDoctor()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string userSpecialty)
        {
            string apiKey = GetAPIKey();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.betterdoctor.com");
            var response = await client.GetAsync($"/2016-03-01/doctors?query={userSpecialty}&specialty_uid=pediatrician&user_key={apiKey}");
            var result = await response.Content.ReadAsAsync<List<Rootobject>>();
            // now we have a list of UID that correspond to doctors with that specialty
            return View("ListDoctors", result);
        }

        public IActionResult Add()
        {
            return View();
        }

        // next, we need to make a method that will pull that stored UID from the Db and 
        // access it somewhere else






        //public IActionResult AddFavorite(Doctor doctor)
        //{
        //    AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
        //    DoctorUid favoriteDr = new DoctorUid();

        //    string newDoctor = SelectedDoctorUid().ToString();
        //    //if (ModelState.IsValid)
        //    //{
        //        favoriteDr.Id += SelectedDoctorUid().ToString();

        //        _context.DoctorUid.Add(favoriteDr);
        //        _context.SaveChanges();
        //        return RedirectToAction("FavoriteMovies");
        //    //}
        //    //return RedirectToAction("Index");
        //}
    }
}
