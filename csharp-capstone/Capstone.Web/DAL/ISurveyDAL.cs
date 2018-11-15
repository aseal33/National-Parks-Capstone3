﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        IList<Surveys> FindSurvey();

        void SaveSurvey(Surveys newSurvey);
    }
}
