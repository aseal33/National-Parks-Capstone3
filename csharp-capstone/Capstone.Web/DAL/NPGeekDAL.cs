
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

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
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
                    parks.Add(park);
                }


                //while (reader.Read())
                //{
                //    actors.Add(MapRowToSurveyResult(reader));
                //}
            }

            return parks;
        }


        public Parks ParkDetails(string parkCode, IList<Parks> parkList)
        {
            foreach (var park in parkList)
            {
                if (park.ParkCode == parkCode)
                {
                    return park;
                }
            }
            return null;
        }

        /// <summary>
        /// Maps a sql data row to a Survey object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        //private ??? MapRowToSurveyResult(SqlDataReader reader)
        //{
        //    return new Actor()
        //    {
        //        FirstName = Convert.ToString(reader["first_name"]),
        //        LastName = Parks.lastname)
        //    };
        //}
    }
}
