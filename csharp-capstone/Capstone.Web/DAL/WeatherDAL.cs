using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherDAL : IWeatherDAL
    {
        private string connectionString;

        public WeatherDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        IList<DailyWeather> weatherForecast = new List<DailyWeather>();

        /// <summary>
        /// Returns a list of daily weather forcasts
        /// </summary>
        /// <param name="WeatherSearch"></param>
        /// <returns></returns>
        public FiveDayWeather FindWeather(string parkCode)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DailyWeather weather = new DailyWeather()
                    {
                        //ParkCode = Convert.ToString(reader["parkcode"]),

                        FiveDayForecastValue = Convert.ToInt32(reader["fivedayforecastvalue"]),

                        Low = Convert.ToInt32(reader["low"]),

                        High = Convert.ToInt32(reader["high"]),

                        Forecast = Convert.ToString(reader["forecast"])
                    };
                    weatherForecast.Add(weather);
                }
            }
            var fiveDay = new FiveDayWeather();

            foreach (var day in weatherForecast)
            {
                if (parkCode == day.ParkCode)
                {
                    fiveDay.weathers.Add(day);
                }
            }

            return fiveDay;
        }

        //public FiveDayWeather CombineWeather(string parkCode)
        //{
        //    var dal = new WeatherDAL(connectionString);
        //    var five = new FiveDayWeather();
        //    var list = new List<DailyWeather>();
        //    IList<DailyWeather> dailyWeathers = dal.FindWeather();

        //    for (int i = 0; i <= dailyWeathers.Count; i++)
        //    {
        //        if (parkCode == dailyWeathers[i].ParkCode)
        //        {
        //            list.Add(dailyWeathers[i]);
        //        }
        //    }

        //    list = five.weathers;
        //    return five;
        //}

        public FiveDayWeather ConvertWeather(FiveDayWeather fiveDay)
        {
            foreach (var day in fiveDay.weathers)
            {
                day.High = ((day.High - 32) * (5 / 9));
                day.Low = ((day.Low - 32) * (5 / 9));
            }

            return fiveDay;
        }
    }
}
