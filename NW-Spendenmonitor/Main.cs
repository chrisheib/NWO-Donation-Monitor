﻿using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace NW_Spendenmonitor
{
    public partial class Main : Form
    {

        SQLiteConnection dbConnection;
        DataTable dt;
        bool showSQLHistory = true;
        bool DEBUG = false;

        public Main()
        {
            InitializeComponent();
            DebugMessageBox("Inititalized");
            
            VersionChecker v = new VersionChecker(new VersionChecker.VersionCheckHandler(ReactToChangedVersion));
            DebugMessageBox("Version checker initialised");

            ChangeHistoryCollapsed(this, null);
            DebugMessageBox("History Collapsed");

            dbConnection = DB.OpenSQLConnection(out string status);
            SetStatus(status);
            dt = new DataTable();
            dataGridView1.DataSource = dt;
            FillPrevious();
            DebugMessageBox("DB opened, Previous filled");

            Languages.form_uilanguages = new System.Collections.Generic.List<string>
            {
                "Deutsch",
                "English"
            };

            Languages.SetLanguage(this, (Languages.UILanguage)Int32.Parse(GetConfig("UILanguage", "0")), true);
            DebugMessageBox("SetLanguage");
            SetComponentLanguage();
            DebugMessageBox("SetComponentLanguage");

            cb_uilanguage.SelectedIndex = (int)ConfigClass.UILanguage;
            DebugMessageBox("cb Language");

            string strDateFrom = GetConfig("FromDate");
            string strDateTo = GetConfig("ToDate");
            DateTime dateFrom;
            DateTime dateTo;

            if (strDateFrom == "")
            {
                dateFrom = DateTime.Now;
            }
            else
            {
                dateFrom = Main.StringToDateTime(strDateFrom);
            }

            if (strDateTo == "")
            {
                dateTo = DateTime.Now.Date.AddDays(1).AddTicks(-1);
            }
            else
            {
                dateTo = Main.StringToDateTime(strDateTo);
            }

            dTPFrom.Value = dateFrom;
            dTPTo.Value = dateTo;
            DebugMessageBox("Dates filled");

            cb_uilanguage.SelectionChangeCommitted += new EventHandler(Cb_uilanguage_SelectedIndexChanged);
            cb_statistic.SelectedIndexChanged += new EventHandler(EventRunStatement);
            cb_importlanguage.SelectionChangeCommitted += new EventHandler(Cb_importlanguage_SelectedIndexChanged);
            dTPFrom.ValueChanged += new EventHandler(EventRunStatement);
            dTPTo.ValueChanged += new EventHandler(EventRunStatement);
            DebugMessageBox("Event Handlers Set");

            Statement.RunStatement(this, Int32.Parse(GetConfig("LastStatistic", "0")));
            DebugMessageBox("Last statement run");

            //ConfigClass.TestConfig(dbConnection);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DB.Execute(dbConnection, textBox1.Text);
            FillPrevious();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            StatementToGrid(textBox1.Text);
        }

        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                InitialDirectory = GetConfig("ImportPath","C:\\"),
                RestoreDirectory = true,
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetStatus(openFileDialog1.FileName + Languages.status_beingimported);
                    
                    string path = openFileDialog1.FileName;
                    SetConfig("ImportPath", Path.GetDirectoryName(path));
                    string oldpath = "";

                    ConfigClass.ImportLanguage = cb_importlanguage.SelectedIndex;

                    //prepare for language
                    switch (ConfigClass.ImportLanguage)
                    {
                        case 0:
                            break;
                        case 1:
                            oldpath = path;
                            ImportLanguageManager.PrepareFileGerman(ref path);
                            break;
                        default:
                            break;
                    }
                    
                    string changedLines = Convert.ToString(DonationImporter.ImportCSVToInput(dbConnection, path, chk_rename.Checked, cb_importlanguage.SelectedIndex, oldpath));
                    SetStatus(Languages.status_importof + openFileDialog1.FileName + Languages.status_importfinished + changedLines + Languages.status_importentries);
                    StatementToGrid("select * from input order by time desc limit " + changedLines, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void EventRunStatement(object sender, EventArgs e)
        {

            Statement.RunStatement(this, cb_statistic.SelectedIndex);
        }

        private void ChangeHistoryCollapsed(object sender, EventArgs e)
        {
            if (showSQLHistory)
            {
                int collapsedSize = 90;

                listBox1.Visible = false;
                splitContainer1.Panel1MinSize = collapsedSize;
                splitContainer1.SplitterDistance = collapsedSize;

                showSQLHistory = !showSQLHistory;
            }
            else
            {
                int expandedSize = 250;

                splitContainer1.Panel1MinSize = expandedSize;
                splitContainer1.SplitterDistance = expandedSize;
                listBox1.Visible = true;

                showSQLHistory = !showSQLHistory;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_sqlhistory.Checked != showSQLHistory)
            {
                ChangeHistoryCollapsed(sender, e);
            }
        }

        private void Cb_uilanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Languages.SetLanguage(this,(Languages.UILanguage)cb_uilanguage.SelectedIndex);
            SetComponentLanguage();
        }

        private void Cb_importlanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetConfig("ImportLanguage", cb_importlanguage.SelectedIndex.ToString());
        }
    }
}