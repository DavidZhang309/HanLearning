using HanLearning.Data;
using System;
using System.Collections.Generic;

namespace HanLearning.Dictionary
{
    public partial class Learning : System.Web.UI.Page
    {
        protected LearningCharacterMapping query;

        protected void Page_Init(object sender, EventArgs e)
        {
            using (HanDatabase db = new HanDatabase())
            {
                query = new LearningCharacterMapping(db, ((HanLearning.Masters.Site)Master).UserID, "zh-HK");
            }
        }

        protected void OnLearn(object sender, EventArgs e)
        {

        }

        protected void OnLearned(object sender, EventArgs e)
        {

        }
    }
}