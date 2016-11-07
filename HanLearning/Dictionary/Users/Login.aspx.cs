using HanLearning.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HanLearning.Dictionary.Users
{
    public partial class Login : System.Web.UI.Page
    {
        protected enum LoginState { Success = 0, NoUsername = 1, BadPassword = 2 }

        protected LoginState state;

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SqlParameter userID = new SqlParameter("UserID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter returnCode = new SqlParameter() { Direction = ParameterDirection.ReturnValue };

                SqlParameter[] parameters = {
                    new SqlParameter("Username", username.Text),
                    new SqlParameter("Password", password.Text),
                    userID,
                    returnCode
                };

                using (HanDatabase db = new HanDatabase())
                {
                    db.ExecuteStoredProcedure("spUserLogin", parameters);
                }
                
                state = (LoginState)returnCode.Value;

                if (state == LoginState.Success)
                {
                    Session["UserID"] = userID.Value;

                    Response.Redirect("/Dictionary/");
                }
            }
        }
    }
}