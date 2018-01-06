using System.Collections.Generic;
using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    
    static class DonationImporter
    {
        public static int ImportCSVToInput(SQLiteConnection dbConnect, string path, bool rename)
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

            foreach (var donationLine in donationList)
            {
                if (string.Compare(donationLine.Time, maxDate) > 0)
                {
                    string donationInputStatement = DonationLineToStatement(donationLine);
                    DB.Execute(dbConnect, donationInputStatement);
                    changedLines++;
                }

                // find last date
                if (string.Compare(donationLine.Time, maxDateInFile) > 0)
                {
                    maxDateInFile = donationLine.Time;
                }

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

            filename = (filename + " " + maxDate + fileextension).Replace(":","-");
            string newPath = System.IO.Path.GetDirectoryName(path) + "\\" + filename;
            System.IO.File.Move(path, newPath);
        }

        private static string DonationLineToStatement(DonationDataLine dataLine)
        {
            string result;

            result = "INSERT INTO input " +
                "(charname, account, time, item, itemcount, resource, resourcequantity, donorsguild, targetguild) VALUES (" +
                "'" + dataLine.Charname + "'" + "," +
                "'" + dataLine.Account + "'" + "," +
                "'" + dataLine.Time + "'" + "," +
                "'" + dataLine.Item + "'" + "," +
                "'" + dataLine.Itemcount + "'" + "," +
                "'" + dataLine.Resource + "'" + "," +
                "'" + dataLine.Resourcequantity + "'" + "," +
                "'" + dataLine.Donorsguild + "'" + "," +
                "'" + dataLine.Targetguild + "'" + ")";

            return result;
        }
    }

    
}
