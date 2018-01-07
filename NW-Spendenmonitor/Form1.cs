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

        public Form1()
        {
            InitializeComponent();
            dbConnection = DB.OpenSQLConnection(out string status);
            SetStatus(status);
            dt = new DataTable();
            dataGridView1.DataSource = dt;
            FillPrevious();

            comboBox1.SelectedIndex = 0;

            dTPFrom.Text = "01.01.2018 00:00:00";
            dTPTo.Text = DateTime.Now.ToString("dd.MM.yyyy") + " 23:59:59";
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
                    SetStatus("Importiere " + openFileDialog1.FileName + ", bitte warten!");
                    string changedLines = Convert.ToString(DonationImporter.ImportCSVToInput(dbConnection, openFileDialog1.FileName, checkBox1.Checked));
                    SetStatus("Import von " + openFileDialog1.FileName + " abgeschlossen, " + changedLines + " Einträge hinzugefügt!");
                    StatementToGrid("select * from input order by time desc limit " + changedLines, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int action = comboBox1.SelectedIndex;

            switch (action)
            {
                case 0:
                    Statement.CountVouchersPerAccount(this);
                    break;
                case 1:
                    Statement.CountInfluencePerAccount(this);
                    break;
                case 2:
                    Statement.CountGemsPerAccount(this);
                    break;
                case 3:
                    Statement.CountInfluencePerDay(this);
                    break;
                case 4:
                    Statement.CountGemsPerDay(this);
                    break;
                default:
                    break;
            }
        }
    }
}
