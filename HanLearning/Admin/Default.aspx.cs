using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HanLearning.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\root\\cloud\\DATA\\unihan\\");
            while ((line = file.ReadLine()) != null)
            {
                
            }

            file.Close();

        }
    }
}