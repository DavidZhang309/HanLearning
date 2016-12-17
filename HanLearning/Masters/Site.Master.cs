using System;
using System.Web.UI;
using HanLearning.Data;
using System.Data;
using System.Text;
using CoreFramework.Extensions;
using System.Data.SqlClient;

namespace HanLearning.Masters
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public int UserID { get; private set; } = -1;
        public bool SelfLearning { get; private set; }
        private void LoadUserSession()
        {
            using (HanDatabase db = new HanDatabase())
            {
                DataTable result = db.ExecuteQuery(
                    "SELECT SelfLearning, PreferredLanguage FROM vUserInfo WHERE UserID=@UserID",
                    new SqlParameter[] { new SqlParameter("UserID", UserID) }
                    );

                if (result.Rows.Count == 1)
                {
                    DataRow userData = result.Rows[0];

                    int selfLearning = -1;
                    if (int.TryParse(userData["SelfLearning"].ForceToString(), out selfLearning))
                    {
                        Session["SelfLearning"] = selfLearning == 1;
                    }

                    string culture = userData["PreferredLanguage"].ForceToString();
                    if (string.IsNullOrWhiteSpace(culture))
                    {
                        culture = null;
                    }
                    Session["PreferredLanguage"] = culture;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            object userID = Session["UserID"];
            if (userID is int)
            {
                UserID = (int)userID;
                object selfLearning = Session["SelfLearning"];

                //if selfLearning is null, user info hash not been loaded in
                if (selfLearning == null)
                {
                    LoadUserSession();
                    selfLearning = Session["SelfLearning"];
                }

                if (selfLearning is bool)
                {
                    SelfLearning = (bool)selfLearning;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Build Culture list
            StringBuilder builder = new StringBuilder();
            using (HanDatabase db = new HanDatabase())
            {
                DataTable data = db.ExecuteQuery("SELECT Culture FROM vCultureList");

                foreach (DataRow row in data.Rows)
                {
                    string culture = row["culture"].ForceToString();
                    builder.AppendFormat("<option value='{0}'>{1}</option>", culture, GetGlobalResourceObject("Literals", culture));
                }
            }
            cultureOptions.Controls.Add(new LiteralControl(builder.ToString()));

        }
    }
}