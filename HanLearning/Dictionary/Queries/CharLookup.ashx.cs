using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using CoreFramework.ASP.Extensions;
using HanLearning.Data;

namespace HanLearning.Dictionary.Queries
{
    /// <summary>
    /// Summary description for CharLookup
    /// </summary>
    public class CharLookup : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            // process request
            XmlDocument request = new XmlDocument();
            try
            {
                request.Load(context.Request.InputStream);
            }
            catch (XmlException)
            {
                return;
            }

            XmlNode cultureNode = request.SelectSingleNode("/query/culture");
            if (cultureNode == null)
            {
                return;
            }
            HashSet<int> chars = new HashSet<int>();
            foreach (XmlNode node in request.SelectNodes("/query/chars/*"))
            {
                int utf = -1;
                if (int.TryParse(node.InnerText, out utf))
                {
                    chars.Add(utf);
                }
            }
            
            // create response
            XmlDocument doc = new XmlDocument();
            XmlElement charList = doc.CreateElement("characters");
            doc.AppendChild(charList);
            
            using (HanDatabase db = new HanDatabase())
            {
                CharacterLookupMapping query = new CharacterLookupMapping(db, chars, cultureNode.InnerText, context.Session.GetSessionInt("UserID", 0));
                foreach (CharacterData charData in query.Characters.Values)
                {
                    //write character information
                    XmlElement charElement = doc.CreateElement("character");
                    charElement.SetAttribute("utf", charData.UTFCode.ToString());
                    charList.AppendChild(charElement);

                    //write readings information
                    XmlElement readingsElement = doc.CreateElement("readings");
                    charElement.AppendChild(readingsElement);

                    foreach (CharacterReading reading in charData.Readings)
                    {
                        XmlElement readingElement = doc.CreateElement("reading");
                        readingElement.InnerText = reading.Reading;
                        readingElement.SetAttribute("source", reading.Source);
                        readingElement.SetAttribute("culture", reading.Culture);
                        readingElement.SetAttribute("system", reading.System);
                        readingsElement.AppendChild(readingElement);
                    }

                    XmlElement definitionsElement = doc.CreateElement("definitions");
                    charElement.AppendChild(definitionsElement);

                    //write definition information
                    foreach (CharacterDefinition definition in charData.Definitions)
                    {
                        XmlElement defElement = doc.CreateElement("definition");
                        defElement.InnerText = definition.Definition;
                        defElement.SetAttribute("source", definition.Source);
                        defElement.SetAttribute("culture", definition.Culture);
                        definitionsElement.AppendChild(defElement);
                    }
                }
            }

            context.Response.ContentType = "text/xml";
            context.Response.Write(doc.OuterXml);
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