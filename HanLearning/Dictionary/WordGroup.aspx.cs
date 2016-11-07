using System;
using HanLearning.Data;

namespace HanLearning.Dictionary
{
    public partial class WordGroup : System.Web.UI.Page
    {
        protected WordGroupMapping Query;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (HanDatabase db = new HanDatabase())
            {
                Query = new WordGroupMapping(db);
            }
        }
    }
}