using System.Collections.Generic;
using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    static class DonationImporter
    {
        public static bool ImportCSVToInput(SQLiteConnection dbConnect, string path)
        {
            bool result = false;

            IEnumerable<DonationDataLine> donationList = CSVReader.ReadCSV(path);
            
            foreach (var donationLine in donationList)
            {
                string donationInputStatement = DonationLineToStatement(donationLine);
                DB.Execute(dbConnect, donationInputStatement);
            }

            return result;
        }

        private static string DonationLineToStatement(DonationDataLine dataLine)
        {
            string result;

            result = "INSERT INTO input " +
                "(charname, account, time, item, itemcount, resource, resourcequantity, donorsguild, targetguild) VALUES (" +
                "'" + dataLine.charname + "'" + "," +
                "'" + dataLine.account + "'" + "," +
                "'" + dataLine.time + "'" + "," +
                "'" + dataLine.item + "'" + "," +
                "'" + dataLine.itemcount + "'" + "," +
                "'" + dataLine.resource + "'" + "," +
                "'" + dataLine.resourcequantity + "'" + "," +
                "'" + dataLine.donorsguild + "'" + "," +
                "'" + dataLine.targetguild + "'" + ")";

            return result;
        }
    }

    
}
