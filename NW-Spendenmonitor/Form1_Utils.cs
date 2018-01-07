using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NW_Spendenmonitor
{
    partial class Form1
    {

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
    }
}
