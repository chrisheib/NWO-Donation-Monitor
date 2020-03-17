using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace NW_Spendenmonitor
{
    partial class Main
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
            btn_export.Text = Languages.form_export;
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

        public void ReactToChangedVersion(object sender, VersionCheckerEventArgs e)
        {
            if ((e.Success) && (string.Compare(e.NewVersion, ConfigClass.VERSION) > 0))
            {
                DebugMessageBox("Versionchecker finished successfully: New Version found.");
                DialogResult dialogResult = MessageBox.Show("Neue Version: " + e.NewVersion + Environment.NewLine +
                    "Aktuelle Version: " + ConfigClass.VERSION + Environment.NewLine +
                    "Update herunterladen?", "Update verfügbar!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://github.com/chrisheib/NWO-Donation-Monitor/releases");
                }
            }
            else if (e.Success)
            {
                DebugMessageBox("Versionchecker finished successfully: No new Version found.");
            }
            else
            {
                DebugMessageBox("Versionchecker finished: Failed checking.");
            }
        }

        public void DebugMessageBox(string message)
        {
            if (DEBUG)
            {
                MessageBox.Show(message);
            }
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            //Ort auswählen
            SaveFileDialog fileDialog1 = new SaveFileDialog()
            {
                InitialDirectory = GetConfig("ExportPath", "C:\\"),
                RestoreDirectory = true,
                Filter = "CSV-File (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 0

            };

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = fileDialog1.FileName;
                    SetConfig("ExportPath", Path.GetDirectoryName(path));
                    SetStatus(fileDialog1.FileName + Languages.status_beingimported);

                    //Statement öffnen
                    if (DB.Select(dbConnection, "SELECT (''''||charname||''','''||account||''','''||time||''','''||item||''','''||itemcount||''','''||resource||''','''||resourcequantity||''','''||donorsguild||''','''||targetguild||'''') from input order by time", out SQLiteDataReader q))
                    {
                        //alles reinschreiben
                        FileStream f = File.OpenWrite(path);
                        
                        Byte[] crlf = StringToByteArray(Environment.NewLine);

                        Byte[] text = StringToByteArray("'Character Name','Account Handle','Time','Item','Item Count','Resource','Resource Quantity','Donors Guild','Recipient Guild'");
                        f.Write(text, 0, text.Length);
                        f.Write(crlf, 0, crlf.Length);

                        while (q.Read())
                        {
                            text = StringToByteArray(q.GetString(0));
                            f.Write(text, 0, text.Length);
                            f.Write(crlf, 0, crlf.Length);
                        }

                        f.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
                }
            }
        }

        private void Btn_export2_Click(object sender, EventArgs e)
        {

            //Ort auswählen
            SaveFileDialog fileDialog1 = new SaveFileDialog()
            {
                InitialDirectory = GetConfig("ExportPath", "C:\\"),
                RestoreDirectory = true,
                Filter = "CSV-File (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 0

            };

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = fileDialog1.FileName;
                    SetConfig("ExportPath", Path.GetDirectoryName(path));
                    SetStatus(fileDialog1.FileName + Languages.status_beingimported);

                    //Statement öffnen

                    GetFromToDates(out string dateFrom, out string dateTo);
                    string statement = "select (SUBSTR('@'|| account || '                   ' , 1, 19) || ':' || SUBSTR('      ' || sum(itemcount), -6, 6) ) " + 
                        " from input where (item like '%voucher%')" +
                        " and (time >= $dateFrom and time <= $dateTo) group by account order by sum(itemcount) desc";
                    SQLiteCommand command = new SQLiteCommand(statement);
                    command.Parameters.AddWithValue("$dateFrom", dateFrom);
                    command.Parameters.AddWithValue("$dateTo", dateTo);

                    if (DB.Select(dbConnection, command, out SQLiteDataReader q))
                    {
                        //alles reinschreiben
                        FileStream f = File.OpenWrite(path);

                        Byte[] crlf = StringToByteArray(Environment.NewLine);

                        Byte[] text;

                        while (q.Read())
                        {
                            text = StringToByteArray(q.GetString(0));
                            f.Write(text, 0, text.Length);
                            f.Write(crlf, 0, crlf.Length);
                        }

                        f.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
                }
            }
        }

        private byte[] StringToByteArray(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);   
        }

        public static DateTime StringToDateTime(string datestring)
        {
            string formats = "yyyy-MM-dd HH:mm:ss";
            CultureInfo culture = new CultureInfo("de-DE");
            DateTime result;

            try
            {
                result = DateTime.ParseExact(datestring, formats, culture);    
            }
            catch (FormatException)
            {
                result = new DateTime(0, 0, 0);
            }
            return result;
        }
    }
}
