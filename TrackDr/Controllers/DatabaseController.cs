using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrackDr.Models;

namespace TrackDr.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly TrackDrDbContext _context;
        private readonly IConfiguration _configuration;
        public DatabaseController(TrackDrDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();

        }
        public bool CanAddDoctor(Doctor doctor)
        {
            Doctor foundDoctor = _context.Doctor.Find(doctor.DoctorId);
            if (foundDoctor.DoctorId == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}