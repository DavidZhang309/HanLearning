using HanLearning.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HanLearning.Tools
{
    public partial class TextTranslator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if postback, no point of continuing if no characters
            if (!IsPostBack)
            {
                return;
            }

            // get charset option
            int parsedcharsetType = 0;
            VariantType charsetType = VariantType.None;
            if (int.TryParse(characterSetOption.SelectedValue, out parsedcharsetType))
            {
                charsetType = (VariantType)parsedcharsetType;
            }

            // collect character set to query
            HashSet<int> charset = new HashSet<int>();
            foreach (char c in textBlock.Text)
            {
                charset.Add(Convert.ToInt32(c));
            }

            //query and render
            using (HanDatabase db = new HanDatabase())
            {
                CharacterLookupMapping query = new CharacterLookupMapping(db, charset, "zh-HK", ((HanLearning.Masters.Site)this.Master).UserID, charsetType);

                string[] blocks = textBlock.Text.Split('\n');
                StringBuilder sb = new StringBuilder();
                foreach (string block in blocks)
                {
                    foreach (char c in block)
                    {
                        CharacterData data;
                        if (query.Characters.TryGetValue(Convert.ToInt32(c), out data)){
                            int resultChar = Convert.ToInt32(c);
                            
                            if (charsetType == data.VariantType && data.Variants.Count > 0)// && query.Characters.ContainsKey(data.Variants[0]))
                            {
                                data = query.Characters[data.Variants[0]];
                                resultChar = data.UTFCode;
                            }

                            if (data.LearningStatus == LearningStatus.Learned)
                            {
                                sb.AppendFormat("<span class=\"learned\">&#{0}</span>", resultChar);
                            }
                            else if (data.LearningStatus == LearningStatus.Learning)
                            {
                                sb.AppendFormat("<span class=\"learning\">&#{0}</span>", resultChar);
                            }
                            else
                            {
                                sb.AppendFormat("<span class=\"not-learned\">&#{0}</span>", resultChar);
                            }
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                    resultArea.Controls.Add(new LiteralControl() { Text = "<p>" + sb.ToString() + "</p>" });
                    sb.Clear();
                }
            }
        }
    }
}