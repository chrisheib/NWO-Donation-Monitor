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
            if (DB.Select(dbConnection, textBox1.Text, out SQLiteDataReader query))
            {
                //dataGridView1.DataSource = dt;
                dt = new DataTable();
                dataGridView1.DataSource = dt;
                dt.Load(query);
            }
            FillPrevious();
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
        
    }
}
