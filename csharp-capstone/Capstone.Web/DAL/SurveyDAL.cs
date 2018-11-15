using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyDAL : ISurveyDAL
    {
        private string connectionString;

        public SurveyDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        IList<Surveys> surveys = new List<Surveys>();

        public Dictionary<string, int> FindSurvey()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT parkName, COUNT('parkCode') as 'votes' FROM survey_result INNER JOIN park ON park.parkCode = survey_result.parkCode GROUP BY parkName ORDER BY votes desc", conn);

                Dictionary<string, int> results = new Dictionary<string, int>();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(Convert.ToString(reader["parkname"]), Convert.ToInt32(reader["votes"]));                    
                }
                return results;
            }
        }

        public Dictionary<Parks, int> ConvertResults(Dictionary<string, int> results, IList<Parks> parks)
        {
            var surveyParks = new Dictionary<Parks, int>();
            foreach (var kvp in results)
            {
                foreach (var park in parks)
                {
                    if (kvp.Key == park.ParkName)
                    {
                        surveyParks.Add(park, kvp.Value);
                    }
                }
            }
            return surveyParks;
        }

        /// <summary>
        // Maps a sql data row to a Survey object.
        // </summary>
        // <param name = "reader" ></ param >
        // < returns ></ returns >
        public void SaveSurvey(Surveys newSurvey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd =new SqlCommand("INSERT INTO survey_results VALUES (@parkcode, @emailaddress, @state, @activitylevel)", conn);
                    cmd.Parameters.AddWithValue("@parkcode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailaddress", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activitylevel", newSurvey.ActivityLevel);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
