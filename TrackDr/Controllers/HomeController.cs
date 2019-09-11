using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult AddDoctor(string doctorUid)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            ParentDoctor savedDoctors = new ParentDoctor();
            if (ModelState.IsValid)
            {
                Doctor newDoctor = new Doctor();
                newDoctor.DoctorId = doctorUid;
                _context.Doctor.Add(newDoctor);
                _context.SaveChanges();


                savedDoctors.ParentId = thisUser.Id;
                savedDoctors.DoctorId = doctorUid;

                _context.ParentDoctor.Add(savedDoctors);
                _context.SaveChanges();
                return View("Search");
            }
            return View("Search");
        }

        public async Task<IActionResult> SavedDoctors()
        {

            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            //Goes to Db, pull list of doctors associated with current user
            List<ParentDoctor> savedList = _context.ParentDoctor.Where(u => u.ParentId == thisUser.Id).ToList();
            //Establishes new list to store doctor UID's
            List<string> doctorIdList = new List<string>();
            //List of list of "Doctors", Rootobject >>> Datum[]
            List<SingleDoctor> doctorList = new List<SingleDoctor>();
           
            //Taking all doctorIDs from parentdoctor relationship, adding them to DoctorId list
            foreach (ParentDoctor relationship in savedList)
            {
                doctorIdList.Add(relationship.DoctorId);
            }
            
            //Taking every doctorid, going to api, bringing back that specific doctor and placing it in list
            foreach (string doctor in doctorIdList)
            {
                doctorList.Add(await GetDoctor(doctor));
            }
            

            //List<Doctor> thisUsersDoctors = _context.Doctor.Where(doctor => doctor.ParentDoctor.All( => thisUser.Id.Contains(id)));
            return View(doctorList);
        }
        public IActionResult Add()
        {
            return View();
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



        //saves the user's address to the UserDb as well as the user's Id number and their ASP Id
       [HttpPost]
        public IActionResult RegisterUser(Parent newUserInfo)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            Parent newUser = _context.Parent.Find(newUserInfo.ParentId);

            newUser.HouseNumber = newUserInfo.HouseNumber;
            newUser.Street = newUserInfo.Street;
            newUser.Street2 = newUserInfo.Street2;
            newUser.City = newUserInfo.City;
            newUser.State = newUserInfo.State;
            newUser.ZipCode = newUserInfo.ZipCode;
            newUser.ParentId = thisUser.Id;

            newUser.PhoneNumber = newUserInfo.PhoneNumber;
            newUser.Email = thisUser.Email;

            

            _context.Parent.Add(newUser);
            _context.SaveChanges();

            return View("Search");
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult UserInformation()
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            Parent found = _context.Parent.Find(thisUser.Id);
            if (found != null)
            {
                return View("UserInformation", found);
            }
            return View("Search");
        }


        [HttpPost]
        public IActionResult EditUserInformation(Parent user)
        {
            // AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();

            Parent updatedUser = new Parent();
            updatedUser.HouseNumber = user.HouseNumber;
            updatedUser.Street = user.Street;
            updatedUser.Street2 = user.Street2;
            updatedUser.City = user.City;
            updatedUser.State = user.State;
            updatedUser.ZipCode = user.ZipCode;
            updatedUser.ParentId = user.ParentId;
            updatedUser.Email = user.Email;
            updatedUser.PhoneNumber = user.PhoneNumber;
            updatedUser.ParentId = user.ParentId;

            _context.Entry(updatedUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(updatedUser);
            _context.SaveChanges();

            

            return View("UserInformation", updatedUser);
        }

        [HttpGet]
        public IActionResult EditUserInformation(string ParentId)
        {
            Parent found = _context.Parent.Find(ParentId);
            if (found != null)
            {
                return View("EditUserInformation", found);

            }

            return View("Search");
        }
    }
}
