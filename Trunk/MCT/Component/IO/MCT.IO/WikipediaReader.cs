using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCT.IO
{
    public class WikipediaReader:WebsiteReader
    {
        public WikipediaReader()
        {
            BaseUrl = @"https://de.wikipedia.org/wiki";
        }

        private void LoadSubjectPage(string name)
        {
            string urlAddress = Path.Combine(BaseUrl,name);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                this.Html = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
        }

        public string GetScientificName(string name)
        {
            string scientificName = "";

            if (String.IsNullOrEmpty(this.Html))
                LoadSubjectPage(name);

            int namePosition = this.Html.IndexOf("<p><b>" + name + "</b>");

            if (namePosition != -1)
            {
                string body = this.Html.Substring(namePosition);

                int start = body.IndexOf('(');
                int end = body.IndexOf(')');

                if (start != -1 && end != -1)
                {
                    scientificName = body.Substring(start, end);
                }
            }

            return scientificName;
        }
    }
}
