using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NW_Spendenmonitor
{
    public partial class Export : Form
    {
        readonly Main mainform;

        public Export(Main main)
        {
            mainform = main;
            InitializeComponent();
        }

        public void export1_Click(object sender, EventArgs e)
        {
            //Ort auswählen
            SaveFileDialog fileDialog1 = new SaveFileDialog()
            {
                InitialDirectory = mainform.GetConfig("ExportPath", "C:\\"),
                RestoreDirectory = true,
                Filter = "CSV-File (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 0
            };

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = fileDialog1.FileName;
                    mainform.SetConfig("ExportPath", Path.GetDirectoryName(path));
                    mainform.SetStatus(fileDialog1.FileName + Languages.status_beingimported);

                    //Statement öffnen
                    if (DB.Select(mainform.dbConnection, "SELECT (''''||charname||''','''||account||''','''||time||''','''||item||''','''||itemcount||''','''||resource||''','''||resourcequantity||''','''||donorsguild||''','''||targetguild||'''') from input order by time", out SQLiteDataReader q))
                    {
                        //alles reinschreiben
                        FileStream f = File.OpenWrite(path);

                        Byte[] crlf = mainform.StringToByteArray(Environment.NewLine);

                        Byte[] text = mainform.StringToByteArray("'Character Name','Account Handle','Time','Item','Item Count','Resource','Resource Quantity','Donors Guild','Recipient Guild'");
                        f.Write(text, 0, text.Length);
                        f.Write(crlf, 0, crlf.Length);

                        while (q.Read())
                        {
                            text = mainform.StringToByteArray(q.GetString(0));
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
            try
            {
                //Statement öffnen

                mainform.GetFromToDates(out string dateFrom, out string dateTo);
                string statement = "select (SUBSTR('@'|| account || '                   ' , 1, 19) || ':' || SUBSTR('      ' || sum(itemcount), -6, 6) ) " +
                    " from input where (item like '%voucher%')" +
                    " and (time >= $dateFrom and time <= $dateTo) group by account order by sum(itemcount) desc";
                SQLiteCommand command = new SQLiteCommand(statement);
                command.Parameters.AddWithValue("$dateFrom", dateFrom);
                command.Parameters.AddWithValue("$dateTo", dateTo);

                if (DB.Select(mainform.dbConnection, command, out SQLiteDataReader q))
                {
                    if (rb_exportfile.Checked)
                    {

                        //Ort auswählen
                        SaveFileDialog fileDialog1 = new SaveFileDialog()
                        {
                            InitialDirectory = mainform.GetConfig("ExportPath", "C:\\"),
                            RestoreDirectory = true,
                            Filter = "CSV-File (*.csv)|*.csv|All Files (*.*)|*.*",
                            FilterIndex = 0

                        };

                        if (fileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string path = fileDialog1.FileName;
                            mainform.SetConfig("ExportPath", Path.GetDirectoryName(path));
                            mainform.SetStatus(fileDialog1.FileName + Languages.status_beingimported);

                            //alles reinschreiben
                            FileStream f = File.OpenWrite(path);

                            Byte[] crlf = mainform.StringToByteArray(Environment.NewLine);
                            Byte[] text;

                            while (q.Read())
                            {
                                text = mainform.StringToByteArray(q.GetString(0));
                                f.Write(text, 0, text.Length);
                                f.Write(crlf, 0, crlf.Length);
                            }

                            f.Close();
                        }
                    }
                    else
                    {
                        string text = "";
                        while (q.Read())
                        {
                            text = text + q.GetString(0) + Environment.NewLine;
                        }
                        Clipboard.SetDataObject(text);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
            }
            
        }
    }
}
