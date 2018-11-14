using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private INPGeekDAL npgeekDAL;
        public HomeController(INPGeekDAL npgeekDAL)
        {
            this.npgeekDAL = npgeekDAL;
        }


        public IActionResult HomePage()
        {
            var parks = npgeekDAL.FindParks();
            return View(parks);
        }

        public IActionResult Details(string parkCode)
        {
            var parkList = npgeekDAL.FindParks();
            var park = npgeekDAL.ParkDetails(parkCode, parkList);
            return View(park);
        }

        public IActionResult Survey()
        {
            return View();
        }

        public IActionResult SurveyResults()
        {
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
