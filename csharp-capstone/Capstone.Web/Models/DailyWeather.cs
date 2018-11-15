﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DailyWeather
    {
        public string ParkCode { get; set; }

        public int FiveDayForecastValue { get; set; }

        public int Low { get; set; }

        public int High { get; set; }

        public int Forecast { get; set; }
    }
}