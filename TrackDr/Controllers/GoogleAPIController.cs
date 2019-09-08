using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrackDr.Models;

namespace TrackDr.Controllers
{
    public class GoogleAPIController : Controller
    {
        private readonly TrackDrDbContext _context;
        private readonly IConfiguration _configuration;
        public GoogleAPIController(TrackDrDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}