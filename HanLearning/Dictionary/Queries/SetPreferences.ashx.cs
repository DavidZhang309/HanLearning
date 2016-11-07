using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HanLearning.Data;

namespace HanLearning.Dictionary.Queries
{
    public class SetPreferences : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {



            HanDatabase db = new HanDatabase();


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}