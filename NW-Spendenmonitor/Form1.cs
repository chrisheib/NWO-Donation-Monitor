using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NW_Spendenmonitor
{
    public partial class Form1 : Form
    {

        SQLiteConnection dbConnection;
        DataTable dt;
        bool showSQLHistory = true;

        public Form1()
        {
            InitializeComponent();

            ChangeHistoryCollapsed(this, null);

            dbConnection = DB.OpenSQLConnection(out string status);
            SetStatus(status);
            dt = new DataTable();
            dataGridView1.DataSource = dt;
            FillPrevious();

            //TODO: combobox with languageselect
            //TODO: save and read selected language from config table
            Languages.SetLanguage(Languages.Language.German);
            SetComponentLanguage(ConfigClass.UILanguage);
            
            cb_statistic.SelectedIndex = 0;
            cb_importlanguage.SelectedIndex = 0;

            dTPFrom.Text = "01.01.2018 00:00:00";
            dTPTo.Text = DateTime.Now.ToString("dd.MM.yyyy") + " 23:59:59";

            cb_statistic.SelectedIndexChanged += new EventHandler(EventRunStatement);
            dTPFrom.ValueChanged += new EventHandler(EventRunStatement);
            dTPTo.ValueChanged += new EventHandler(EventRunStatement);

            CheckForNewVersion();

            Statement.RunStatement(this, 0);
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
                InitialDirectory = "E:\\Neverwinterlogs",
                RestoreDirectory = true,
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetStatus(openFileDialog1.FileName + Languages.status_beingimported);

                    string path = openFileDialog1.FileName;
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
    }
}
