using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TrackDr.Controllers
{
    public class GoogleAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}