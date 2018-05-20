using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System;

namespace NW_Spendenmonitor
{
    
    static class DonationImporter
    {
        public static int ImportCSVToInput(SQLiteConnection dbConnect, string path, bool rename, int language, string oldpath)
        {

            List<DonationDataLine> donationList = CSVReader.ReadCSV(path);
            donationList.Reverse();

            string maxDate;

            if (DB.Select(dbConnect, "select ifnull(max(time),'0000-00-00 00:00:00') maxtime from input", out SQLiteDataReader query) && query.Read())
            {
                maxDate = (string) query["maxtime"];
            }
            else
            {
                maxDate = "";
            }

            string maxDateInFile = "";
            int changedLines = 0;
            bool importEmptyItems = false;

            foreach (var donationLine in donationList)
            {
            
                if (string.Compare(donationLine.Time, maxDate) > 0)
                {
                    if (donationLine.Item == "" && !importEmptyItems)
                    {
                        string dialogText = 
                            "Es wurde ein Gegenstand gefunden, der scheinbar nicht richtig exportiert wurde." + Environment.NewLine +
                            Environment.NewLine +
                            donationLine.Charname + "@" + donationLine.Account + ", " + donationLine.Time + ", " + donationLine.Resource  + Environment.NewLine +
                            Environment.NewLine +
                            "Versuche, den Export im Spiel erneut zu starten, damit müssten die Lücken geschlossen werden." + Environment.NewLine +
                            "Soll der Import fortgesetzt werden?";
                        DialogResult dialogResult = MessageBox.Show(dialogText, "Spendenmonitor", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            importEmptyItems = true;
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                    
                    DB.Execute(dbConnect, DonationLineToStatement(donationLine), false);
                    changedLines++;
                }
                
                // find last date
                if (string.Compare(donationLine.Time, maxDateInFile) > 0)
                {
                    maxDateInFile = donationLine.Time;
                }
            }
            
            if (oldpath != "")
            {
                System.IO.File.Delete(path);
                path = oldpath;
            }

            if (rename)
            {
                RenameFile(path, maxDateInFile);
            }

            return changedLines;
        }

        private static void RenameFile(string path, string maxDate)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(path);
            string fileextension = System.IO.Path.GetExtension(path);
            bool ok = true;
            try {
                filename = (filename + " " + maxDate + fileextension).Replace(":", "-");
                string newPath = System.IO.Path.GetDirectoryName(path) + "\\" + filename;
                if (System.IO.File.Exists(newPath))
                {
                    string dialogText = Languages.message_renamefailed;
                    DialogResult dialogResult = MessageBox.Show(dialogText, "Spendenmonitor", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.IO.File.Move(newPath, newPath.Replace(System.IO.Path.GetExtension(newPath), " old" + System.IO.Path.GetExtension(newPath)));
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    System.IO.File.Move(path, newPath);
                }
            }
            catch
            {
                string dialogText = "Es ist ein Fehler aufgetreten, die Datei wurde nicht umbenannt.";
                DialogResult dialogResult = MessageBox.Show(dialogText, "Spendenmonitor", MessageBoxButtons.OK);
            }
        }

        private static SQLiteCommand DonationLineToStatement(DonationDataLine dataLine)
        {
            string sql = "INSERT INTO input " +
                "(charname, account, time, item, itemcount, resource, resourcequantity, donorsguild, targetguild) VALUES (" +
                "$charname," +
                "$account," +
                "$time" +
                "$item," +
                "$itemcount," +
                "$resource," +
                "$resourcequantity," +
                "$donorsguild," +
                "$targetguild)";

            SQLiteCommand command = new SQLiteCommand(sql);
            command.Parameters.AddWithValue("$charname", dataLine.Charname);
            command.Parameters.AddWithValue("$account", dataLine.Account);
            command.Parameters.AddWithValue("$time", dataLine.Time);
            command.Parameters.AddWithValue("$item", dataLine.Item);
            command.Parameters.AddWithValue("$itemcount", dataLine.Itemcount);
            command.Parameters.AddWithValue("$resource", dataLine.Resource);
            command.Parameters.AddWithValue("$resourcequantity", dataLine.Resourcequantity);
            command.Parameters.AddWithValue("$donorsguild", dataLine.Donorsguild);
            command.Parameters.AddWithValue("$targetguild", dataLine.Targetguild);
            
            return command;
        }
    }

    
}
