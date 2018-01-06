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
            dbConnection = DB.OpenSQLConnection();
            dt = new DataTable();
            dataGridView1.DataSource = dt;
            FillPrevious();
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
            if (textBox2.Text == "")
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
                        textBox2.Text = openFileDialog1.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
            else
            {
                DonationImporter.ImportCSVToInput(dbConnection,textBox2.Text);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            dTPVon.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTPBis.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            string dateFrom = dTPVon.Text;
            string dateTo = dTPBis.Text;
            dTPVon.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dTPBis.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where item like '%voucher%'"+
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Gutscheinanzahl desc";
            StatementToGrid(statement, false);
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
    }
}
