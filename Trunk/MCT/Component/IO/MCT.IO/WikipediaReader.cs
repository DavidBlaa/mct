using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MCT.IO
{
    public class WikipediaReader : WebsiteReader
    {
        public WikipediaReader()
        {
            BaseUrl = @"https://de.wikipedia.org/wiki";
        }

        private bool LoadSubjectPage(string name)
        {

            string urlAddress = Path.Combine(BaseUrl, name);


            try
            {
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

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public string GetScientificName(string name)
        {
            string scientificName = "";

            if (LoadSubjectPage(name))
            {
                int namePosition = this.Html.IndexOf("<p><b>" + name + "</b>");

                if (namePosition != -1)
                {
                    string body = this.Html.Substring(namePosition);

                    string start = body.Split('(').ElementAt(1);
                    string test = start.Split(')').ElementAt(0);

                    scientificName = test.Replace("<i>", "").Replace("</i>", "");
                    scientificName = scientificName.Split('<').ElementAt(0);
                }
            }

            // Name_(Pflanzengattung)
            if (string.IsNullOrEmpty(scientificName) || scientificName.Equals("Pflanzengattung"))
            {
                if (LoadSubjectPage(name + "_(Pflanzengattung)"))
                {
                    int namePosition = this.Html.IndexOf("<p><b>" + name + "</b>");

                    if (namePosition != -1)
                    {
                        string body = this.Html.Substring(namePosition);

                        string start = body.Split('(').ElementAt(1);
                        string test = start.Split(')').ElementAt(0);

                        scientificName = test.Replace("<i>", "").Replace("</i>", "");
                        scientificName = scientificName.Split('<').ElementAt(0);
                    }
                }

            }

            return scientificName;
        }
    }
}
