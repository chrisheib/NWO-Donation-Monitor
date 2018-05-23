using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

// code taken from: https://stackoverflow.com/questions/6644247/simple-custom-event - themartinmcfly 

namespace NW_Spendenmonitor
{

    public class VersionCheckerEventArgs : EventArgs
    {
        public string NewVersion{ get; private set; }
        public bool Success { get; private set; }

        public VersionCheckerEventArgs(string newVersion, bool success)
        {
            NewVersion = newVersion;
            Success = success;
        }
    }

    class VersionChecker
    {
        public delegate void VersionCheckHandler(object sender, VersionCheckerEventArgs e);
        public event VersionCheckHandler OnVersionCheckComplete;

        public VersionChecker(VersionCheckHandler e)
        {
            OnVersionCheckComplete += e;
            //new thread 
            System.Threading.Thread newThread =
                new System.Threading.Thread(CheckForNewVersion);
            newThread.Start();
        }

        private void VersionCheckerResponse(string newVersion, bool success)
        {
            // Make sure someone is listening to event
            if (OnVersionCheckComplete == null) return;

            VersionCheckerEventArgs args = new VersionCheckerEventArgs(newVersion, success);
            OnVersionCheckComplete(this, args);
        }

        private void CheckForNewVersion()
        {
            bool success = false;
            string version = "";
            try
            {
                string url = "https://api.github.com/repos/chrisheib/NWO-Donation-Monitor/releases";
                string response = Get(url);
                string searchString = "NWO-Donation-Monitor/releases/tag/";
                int a = response.IndexOf(searchString);
                string versionRaw = response.Substring(a + searchString.Length, a + searchString.Length + 10);
                version = versionRaw.Split('\"')[0];
                success = true;
            }
            catch
            {

            }

            VersionCheckerResponse(version, success);

        }

        public string Get(string uri)
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
