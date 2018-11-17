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
        private ISurveyDAL surveyDAL;
        //private IWeatherDAL weatherDAL;
        public HomeController(INPGeekDAL npgeekDAL, ISurveyDAL surveyDAL)
        {
            this.npgeekDAL = npgeekDAL;
            this.surveyDAL = surveyDAL;
            //this.weatherDAL = weatherDAL;
        }

        public IActionResult HomePage()
        {
            var parks = npgeekDAL.FindParks();
            return View(parks);
        }

        [HttpGet]
        public IActionResult Details(string parkCode)
        {
            var parkList = npgeekDAL.FindParks();
            var park = npgeekDAL.ParkDetails(parkCode, parkList);
            //string tempUnit = getCurrentTempUnit();
            //if (tempUnit == "c")
            //{
            //    park.Weather = npgeekDAL.ConvertWeather(park.Weather);
            //}

            return View(park);
        }

        //ViewBag.Message = "";
        //ViewData["Parks"] = npgeekDAL.FindParks();
        //ViewData[""]

        [HttpPost]
        public IActionResult Details(string tempUnit, string parkCode)
        {
            //SaveTempUnit(tempUnit);
            return RedirectToAction("Details(parkCode)");
        }

        //private string GetCurrentTempUnit()
        //{
        //    throw new NotImplementedException();
        //}

        public IActionResult Survey()
        {
            Surveys survey = new Surveys();
            return View(survey);
        }

        public IActionResult SurveyResults()
        {            
            var parks = npgeekDAL.FindParks();
            var reviews = surveyDAL.FindSurvey();
            var surveyParks = surveyDAL.ConvertResults(reviews, parks);
            return View(surveyParks);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveSurvey(Surveys newSurvey)
        {
            surveyDAL.SaveSurvey(newSurvey);
            return RedirectToAction("SurveyResults");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
