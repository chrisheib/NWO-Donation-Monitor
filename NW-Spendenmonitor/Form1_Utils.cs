using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace NW_Spendenmonitor
{
    partial class Form1
    {

        public void StatementToGrid(string statement)
        {
            StatementToGrid(statement, true);
        }

        public void StatementToGrid(string statement, bool logging)
        {
            StatementToGrid(new SQLiteCommand(statement), logging);
        }
        public void StatementToGrid(SQLiteCommand command, bool logging)
        {
            if (DB.Select(dbConnection, command, out SQLiteDataReader query, logging))
            {
                dt = new DataTable();
                dataGridView1.DataSource = dt;
                dt.Load(query);
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            FillPrevious();
        }

        public void GetFromToDates(out string dateFrom, out string dateTo)
        {
            dateFrom = dTPFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");
            dateTo = dTPTo.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void SetStatus(string status)
        {
            textBox2.Text = status;
        }

        private void FillPrevious()
        {
            if (DB.Select(dbConnection, "select distinct command from commands order by id desc limit 15", out SQLiteDataReader query, false))
            {
                listBox1.Items.Clear();
                while (query.Read())
                {
                    listBox1.Items.Add(query.GetString(0));
                }
            }
        }

        private void SetComponentLanguage()
        {
            Text = Languages.form_caption;
            lbl_status.Text = Languages.form_status;
            lbl_importlanguage.Text = Languages.form_importlanguage;
            chk_rename.Text = Languages.form_renamefile;
            btn_import.Text = Languages.form_import;
            btn_sqlhistory.Text = Languages.form_sqlhistory;
            FillComboboxFromList(cb_importlanguage, Languages.form_importlanguages);
            FillComboboxFromList(cb_statistic, Languages.form_commands);
            FillComboboxFromList(cb_uilanguage, Languages.form_uilanguages);

            cb_statistic.SelectedIndex = Int32.Parse(GetConfig("LastStatistic", "0"));
            cb_importlanguage.SelectedIndex = Int32.Parse(GetConfig("ImportLanguage","0"));
            cb_uilanguage.SelectedIndex = (int)ConfigClass.UILanguage;
        }

        private void FillComboboxFromList(ComboBox cb, List<string> list)
        {
            int rememberIndex = cb.SelectedIndex;
            cb.Items.Clear();
            foreach (var s in list)
            {
                cb.Items.Add(s);
            }
        }

        private void CheckForNewVersion()
        {
            try
            {
                string url = "https://api.github.com/repos/chrisheib/NWO-Donation-Monitor/releases";
                string response = Get(url);
                string searchString = "NWO-Donation-Monitor/releases/tag/";
                int a = response.IndexOf(searchString);
                string versionRaw = response.Substring(a + searchString.Length, a + searchString.Length + 10);
                string version = versionRaw.Split('\"')[0];
                if (string.Compare(version, ConfigClass.VERSION) > 0)
                {
                    MessageBox.Show("Neue Version: " + version + Environment.NewLine + "Aktuelle Version: " + ConfigClass.VERSION + Environment.NewLine + "Updaten?");
                }
            }
            catch (Exception)
            {
                
            }
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;

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

        public void SetConfig(string key, string value)
        {
            ConfigClass.SetConfig(dbConnection, key, value);
        }

        public string GetConfig(string key)
        {
            return ConfigClass.GetConfig(dbConnection, key);
        }
        public string GetConfig(string key, string defaultValue)
        {
            return ConfigClass.GetConfig(dbConnection, key, defaultValue);
        }
        
    }

}
