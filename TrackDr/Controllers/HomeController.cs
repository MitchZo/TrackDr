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
using TrackDr.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TrackDr.Controllers
{
    [Authorize] // this allows you to access onnly if you are logged in 
    // can also add [allowanon] to be viewed
    public class HomeController : Controller
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly IGAPIHelper _gAPIHelper;
        private readonly IBDAPIHelper _bDAPIHelper;
        public HomeController(IDatabaseHelper dbHelper, IGAPIHelper gAPIHelper, IBDAPIHelper bDAPIHelper)
        {
            _dbHelper = dbHelper;
            _gAPIHelper = gAPIHelper;
            _bDAPIHelper = bDAPIHelper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
           // AspNetUsers currentUser = _dbHelper.GetCurrentUser(User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                AspNetUsers currentUser = _dbHelper.GetCurrentUser(User.Identity.Name);
                if (_dbHelper.FindParentById(currentUser.Id) == null)
                {
                    return View("RegisterUser");
                }
            }
            return View("Search");
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult ListDoctor()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Search(string userInput, string userState, string userInsurance)
        {
            Rootobject result;
            //if (userState != null)
            //{
            //    result = await _bDAPIHelper.GetDoctorListByState(userInput, userState);

            //}
            //else if (userInsurance != null)
            //{
            //    result = await _bDAPIHelper.GetDoctorListByInsurance(userInput, userState, userInsurance);
            //}
            //else
            //{
            //    result = await _bDAPIHelper.GetDoctorListByState(userInput, userState);
            //}
            result = await _bDAPIHelper.GetDoctorList(userInput, userState, userInsurance);
            return View("ListDoctors", result);
        }


        public IActionResult AddDoctor(string doctorUid)
        {
            AspNetUsers thisUser = _dbHelper.GetCurrentUser(User.Identity.Name);
            ParentDoctor newParentDoctor = new ParentDoctor();
            if (ModelState.IsValid)
            {
                Doctor newDoctor = new Doctor();
                newDoctor.DoctorId = doctorUid;
                if (_dbHelper.CanAddDoctor(newDoctor))
                {
                    _dbHelper.AddNewDoctor(newDoctor);
                }

                newParentDoctor.ParentId = thisUser.Id;
                newParentDoctor.DoctorId = doctorUid;

                if (_dbHelper.CanAddParentDoctorRelationship(thisUser.Id, newDoctor.DoctorId))
                {
                    _dbHelper.AddNewParentDoctorRelationship(newParentDoctor);

                }
                return RedirectToAction("SavedDoctors"); 
            }
            return View("Search");
        }

        public IActionResult SavedDoctors()
        {
            List<SingleDoctor> doctorList = _dbHelper.GetListOfCurrentUsersDoctors(User.Identity.Name);

            return View(doctorList);
        }

        public async Task<IActionResult> DoctorDetails(string doctorId)
        {
            SingleDoctor doctor = await _bDAPIHelper.GetDoctor(doctorId);
            return View(doctor);
        }
        public IActionResult DeleteDoctor(string doctorId)
        {
            var userDelete = _dbHelper.GetCurrentUser(User.Identity.Name);
            var foundDoctor = _dbHelper.FindParentDoctorRelationship(doctorId, userDelete);
            _dbHelper.DeleteDoctor(foundDoctor);
            return RedirectToAction("SavedDoctors");
        }




        //saves the user's address to the UserDb as well as the user's Id number and their ASP Id
        [HttpPost]
        public IActionResult RegisterUser(Parent newUserInfo)
        {
            AspNetUsers thisUser = _dbHelper.GetCurrentUser(User.Identity.Name);
            Parent newUser = new Parent();

            newUser.HouseNumber = newUserInfo.HouseNumber;
            newUser.Street = newUserInfo.Street;
            newUser.Street2 = newUserInfo.Street2;
            newUser.City = newUserInfo.City;
            newUser.State = newUserInfo.State;
            newUser.ZipCode = newUserInfo.ZipCode;
            newUser.ParentId = thisUser.Id;
            newUser.PhoneNumber = newUserInfo.PhoneNumber;
            newUser.Email = thisUser.Email;


            _dbHelper.AddNewParent(newUser);

            return View("Search");
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult UserInformation()
        {
            Parent foundParent = _dbHelper.FindParentById(_dbHelper.GetCurrentUser(User.Identity.Name).Id);
            if (foundParent != null)
            {
                return View("UserInformation", foundParent);
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

            _dbHelper.UpdateParent(updatedUser);
            

            return View("UserInformation", updatedUser);
        }

        [HttpGet]
        public IActionResult EditUserInformation(string ParentId)
        {
            Parent found = _dbHelper.FindParentById(ParentId);
            if (found != null)
            {
                return View("EditUserInformation", found);

            }

            return View("Search");
        }
    }
}

// when the user adds in a new docto we will save the iod the first na,e and the last name
//when we display  that information to the saved deoctors list we can display the first and laset name from the database
//and when we click display extra information we ping to the api to get that additional information 