using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Web;
using System.Data;
using CoreFramework.ASP;
using CoreFramework.ASP.Extensions;
using HanLearning.Data;
using System.Web.SessionState;

namespace HanLearning.Dictionary.Queries
{
    /// <summary>
    /// Summary description for LearningUpdate
    /// </summary>
    public class LearningUpdate : QueryHandler, IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            SetupHandler(context);

            XmlNode characterNode = Request.SelectSingleNode("query/character");
            if (characterNode == null) {
                WriteError(context, "missing_arg", "Missing character argument");
                return;
            }
            int utfCode = -1;
            if (!int.TryParse(characterNode.InnerText, out utfCode))
            {
                WriteError(context, "invalid_arg", "Invalid character");
                return;
            }
            XmlNode intentNode = Request.SelectSingleNode("query/intent");
            if (intentNode == null)
            {
                WriteError(context, "missing_arg", "Missing intent argument");
                return;
            }
            int intent = -1;
            if (!int.TryParse(intentNode.InnerText, out intent))
            {
                WriteError(context, "invalid_arg", "Invalid intent");
                return;
            }

            SqlParameter returnCode = new SqlParameter() { Direction = ParameterDirection.ReturnValue };
            SqlParameter[] parameters =
            {
                new SqlParameter("UserID", context.GetUserID()),
                new SqlParameter("UTFCode", utfCode),
                new SqlParameter("Intent", intent),
                returnCode
            };

            using (HanDatabase db = new HanDatabase())
            {
                db.ExecuteStoredProcedure("spUserLearningUpdate", parameters);
            }

            if ((int)returnCode.Value != 0)
            {
                WriteError(context, "op_failed", "Operation failed with error code: " + returnCode.Value);
                return;
            }

            Response.Save(context.Response.OutputStream);
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