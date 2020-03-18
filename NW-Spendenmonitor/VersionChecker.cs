using System.Net;
using System.IO;

namespace NW_Spendenmonitor
{
    static class VersionChecker
    {
        public static bool completed;
        public static bool success;
        public static string newVersion;

        public static void StartVersionCheckingTask()
        {
            new System.Threading.Thread(CheckForNewVersion).Start();
        }

        private static void CheckForNewVersion()
        {
            try
            {
                string url = "https://api.github.com/repos/chrisheib/NWO-Donation-Monitor/releases";
                string response = Get(url);
                string searchString = "NWO-Donation-Monitor/releases/tag/";
                int a = response.IndexOf(searchString);
                string versionRaw = response.Substring(a + searchString.Length, a + searchString.Length + 10);
                newVersion = versionRaw.Split('\"')[0];
                success = true;
            }
            catch
            {
                // Failure shouldnt disturb the user!
            }
            completed = true;
        }

        public static string Get(string uri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            request.Method = "GET";
            request.AllowAutoRedirect = true;
            request.ProtocolVersion = HttpVersion.Version10;
            request.UserAgent = "NWO-Donationmonitor";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
