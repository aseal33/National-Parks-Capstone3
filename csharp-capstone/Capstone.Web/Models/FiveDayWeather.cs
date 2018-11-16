using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class FiveDayWeather
    {
        //public DailyWeather Day1 { get; set; }

        //public DailyWeather Day2 { get; set; }

        //public DailyWeather Day3 { get; set; }

        //public DailyWeather Day4 { get; set; }

        //public DailyWeather Day5 { get; set; }
        var dal = new WeatherDAL(connectionString);

        public List<DailyWeather> weathers = WeatherDAL.FindWeather();
    }
}
