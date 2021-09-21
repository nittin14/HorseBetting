using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

namespace VKATalk.Common
{
    public static class CommonMethods
    {

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="YourClassObject"></param>
        /// <returns></returns>
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static string ConvertInSentenceCase(string userinputvalue)
        {
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            return (r.Replace(userinputvalue.ToLower(), s => s.Value.ToUpper()));
        }

        public static string CurrentDate()
        {
            var dob = Convert.ToString(DateTime.Now);
            var currentdate = string.Empty;
            string[] dobbreak;
            string[] dobbreakyear;
            if (dob.Contains("/"))
                dobbreak = dob.Split('/');
            else
                dobbreak = dob.Split('-');
            if (dobbreak.Length == 3 && dobbreak[2].Length >= 4)
            {
                if(dobbreak[2].Length> 4)
                {
                    dobbreakyear = dobbreak[2].Split(' ');
                    currentdate = ((dobbreak[0].Length == 1) ? "0" + dobbreak[0] : dobbreak[0]) + "-" + ((dobbreak[1].Length == 1) ? "0" + dobbreak[1] : dobbreak[1]) + "-" + dobbreakyear[0];
                }
               // ViewState["Year"] = dobbreak[2];
            }
            return (currentdate);
        }
    }
}