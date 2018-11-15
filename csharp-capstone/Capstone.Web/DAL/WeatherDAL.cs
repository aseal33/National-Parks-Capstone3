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

        IList<Weather> weatherForecast = new List<Weather>();

        /// <summary>
        /// Returns a list of actors by last name search.
        /// </summary>
        /// <param name="WeatherSearch"></param>
        /// <returns></returns>
        public IList<Weather> FindWeather()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM weather", conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Weather weather = new Weather()
                    {
                        ParkCode = Convert.ToString(reader["parkcode"]),

                        FiveDayForecastValue = Convert.ToInt32(reader["fivedayforecastvalue"]),

                        Low = Convert.ToInt32(reader["low"]),

                        High = Convert.ToInt32(reader["high"]),

                        Forecast = Convert.ToInt32(reader["forecast"])
                    };
                    weatherForecast.Add(weather);
                }
            }
            return weatherForecast;
        }
    }
}
