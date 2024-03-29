﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface INPGeekDAL
    {
        IList<Parks> FindParks();
        Parks ParkDetails(string id, IList<Parks> parkList);
        List<DailyWeather> ConvertWeather(List<DailyWeather> fiveDay);
    }
}
