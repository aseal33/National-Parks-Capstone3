using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        Dictionary<string, int> FindSurvey();

        void SaveSurvey(Surveys newSurvey);

        Dictionary<Parks, int> ConvertResults(Dictionary<string, int> results, IList<Parks> parks);
    }
}
