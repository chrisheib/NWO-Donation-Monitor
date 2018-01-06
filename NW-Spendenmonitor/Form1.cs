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
                    StatementToGrid("select * from input order by time limit " + changedLines, true);
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
                    Statement_CountVouchers();
                    break;
                case 1:
                    Statement_CountInfluence();
                    break;
                case 2:
                    Statement_CountGems();
                    break;
                default:
                    break;
            }

        }

        private void StatementToGrid(string statement)
        {
            StatementToGrid(statement, true);
        }

        private void StatementToGrid(string statement, bool logging)
        {
            if (DB.Select(dbConnection, statement, out SQLiteDataReader query, logging))
            {
                dt = new DataTable();
                dataGridView1.DataSource = dt;
                dt.Load(query);
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            FillPrevious();
        }

        private void Statement_CountInfluence()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Einfluss desc";
            StatementToGrid(statement, false);
        }

        private void Statement_CountVouchers()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where item like '%voucher%'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Gutscheinanzahl desc";
            StatementToGrid(statement, false);
        }

        private void Statement_CountGems()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Juwelen desc";
            StatementToGrid(statement, false);

        }

        private void GetFromToDates(out string dateFrom, out string dateTo)
        {
            dTPVon.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTPBis.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateFrom = dTPVon.Text;
            dateTo = dTPBis.Text;
            dTPVon.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dTPBis.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        }

        private void SetStatus(string status)
        {
            textBox2.Text = status;
        }
    }
}
