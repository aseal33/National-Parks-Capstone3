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

        public IList<Surveys> FindSurvey()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM survey_result", conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Surveys survey = new Surveys()
                    {
                        SurveyId = Convert.ToInt32(reader["surveyid"]),

                        ParkCode = Convert.ToString(reader["parkcode"]),

                        EmailAddress = Convert.ToString(reader["emailaddress"]),

                        State = Convert.ToString(reader["state"]),

                        ActivityLevel = Convert.ToString(reader["activitylevel"])
                    };
                    surveys.Add(survey);
                }
            }
            return surveys;
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
