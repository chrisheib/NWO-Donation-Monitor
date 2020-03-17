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

        public void Btn_Export_Click(object sender, EventArgs e)
        {
            if (rb_export_curSel.Checked)
            {
                Export_Current_Selection();
            }
            else if (rb_export_vouchers.Checked)
            {
                Export_Siege_Vouchers();
            }
        }

        public void Export_Current_Selection()
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

                        MessageBox.Show(Languages.export_success_file);

                        // Close form after successfull export
                        Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(Languages.export_failed + ex.Message);
                }
            }
        }

        private void Export_Siege_Vouchers()
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
                            MessageBox.Show(Languages.export_success_file);
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
                        MessageBox.Show(Languages.export_success_clipboard);
                    }

                    // Close form after successfull export
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Languages.export_failed + ex.Message);
            }
            
        }
    }
}
