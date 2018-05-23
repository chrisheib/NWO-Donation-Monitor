using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;

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
                DialogResult dialogResult = MessageBox.Show("Neue Version: " + e.NewVersion + Environment.NewLine +
                    "Aktuelle Version: " + ConfigClass.VERSION + Environment.NewLine +
                    "Update herunterladen?", "Update verfügbar!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://github.com/chrisheib/NWO-Donation-Monitor/releases");
                }
            }
        }
    }
}
