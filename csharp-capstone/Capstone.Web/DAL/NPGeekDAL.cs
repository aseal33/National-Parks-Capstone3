
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class NPGeekDAL : INPGeekDAL
    {
        private string connectionString;

        public NPGeekDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        IList<Parks> parks = new List<Parks>();


        /// <summary>
        /// Returns a list of actors by last name search.
        /// </summary>
        /// <param name="lastNameSearch"></param>
        /// <returns></returns>
        public IList<Parks> FindParks()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                string code = "";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    code = Convert.ToString(reader["parkCode"]);

                    Parks park = new Parks()
                    {
                        ParkCode = Convert.ToString(reader["parkcode"]),

                        ParkName = Convert.ToString(reader["parkname"]),

                        State = Convert.ToString(reader["State"]),

                        ElevationInFeet = Convert.ToInt32(reader["elevationinfeet"]),

                        Acreage = Convert.ToInt32(reader["acreage"]),

                        MilesOfTrail = Convert.ToDouble(reader["milesoftrail"]),

                        NumberOfCampsites = Convert.ToInt32(reader["numberofcampsites"]),

                        Climate = Convert.ToString(reader["climate"]),

                        YearFounded = Convert.ToInt32(reader["yearfounded"]),

                        AnnualVisitorCount = Convert.ToInt32(reader["annualvisitorcount"]),

                        InspirationalQuote = Convert.ToString(reader["inspirationalquote"]),

                        InspirationalQuoteSource = Convert.ToString(reader["inspirationalquotesource"]),

                        ParkDescription = Convert.ToString(reader["parkdescription"]),

                        EntryFee = Convert.ToInt32(reader["entryfee"]),

                        NumberOfAnimalSpecies = Convert.ToInt32(reader["numberofanimalspecies"])
                    };

                    park.Weather = FindWeather(code);
                    parks.Add(park);

                }
            }
            return parks;
        }

        /// <summary>
        /// Returns a list of daily weather forcasts for the FindParks method
        /// </summary>
        /// <param name="WeatherSearch"></param>
        /// <returns></returns>
        public List<DailyWeather> FindWeather(string parkCode)
        {

            List<DailyWeather> weatherForecast = new List<DailyWeather>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode=@parkCode ORDER BY parkCode", conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DailyWeather weather = new DailyWeather()
                    {
                        ParkCode = parkCode,

                        FiveDayForecastValue = Convert.ToInt32(reader["fivedayforecastvalue"]),

                        Low = Convert.ToInt32(reader["low"]),

                        High = Convert.ToInt32(reader["high"]),

                        Forecast = Convert.ToString(reader["forecast"])
                    };
                    weatherForecast.Add(weather);
                }
            }
            return weatherForecast;
        }    

        public Parks ParkDetails(string parkCode, IList<Parks> parkList)
        {
            Parks newPark = new Parks();
            foreach (var park in parkList)
            {
                if (park.ParkCode == parkCode)
                {
                    newPark = park;
                }
            }
            return newPark;
        }

        public List<DailyWeather> ConvertWeather(List<DailyWeather> fiveDay)
        {
            foreach (var day in fiveDay)
            {
                day.High = ((day.High - 32) * (5 / 9));
                day.Low = ((day.Low - 32) * (5 / 9));
            }

            return fiveDay;
        }
    }
}
