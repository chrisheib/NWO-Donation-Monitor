using System.Collections.Generic;
using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    
    static class DonationImporter
    {
        public static bool ImportCSVToInput(SQLiteConnection dbConnect, string path)
        {
            bool result = false;

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

            foreach (var donationLine in donationList)
            {
                if (string.Compare(donationLine.Time, maxDate) > 0)
                {
                    string donationInputStatement = DonationLineToStatement(donationLine);
                    DB.Execute(dbConnect, donationInputStatement);
                }

            }

            return result;
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
