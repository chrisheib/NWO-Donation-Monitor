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

        private void button1_Click(object sender, EventArgs e)
        {
            DB.Execute(dbConnection, textBox1.Text);
            FillPrevious();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteDataReader query;
            if (DB.Select(dbConnection, textBox1.Text, out query))
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
            SQLiteDataReader query;
            if (DB.Select(dbConnection, "select distinct command from commands order by id desc limit 15", out query, false))
            {
                listBox1.Items.Clear();
                while (query.Read())
                {
                    listBox1.Items.Add(query.GetString(0));
                }
            }
            
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }
    }
}
