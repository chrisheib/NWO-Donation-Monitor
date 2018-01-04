using System.Collections.Generic;
using LINQtoCSV; //http://www.aspnetperformance.com/post/LINQ-to-CSV-library.aspx

namespace NW_Spendenmonitor
{
    class DonationDataLine
    {
        [CsvColumn(Name ="Character Name")]
        public string charname;
        [CsvColumn(Name = "Account Handle")]
        public string account;
        [CsvColumn(Name = "Time")]
        public string time;
        [CsvColumn(Name = "Item")]
        public string item;
        [CsvColumn(Name = "Item Count")]
        public string itemcount;
        [CsvColumn(Name = "Resource")]
        public string resource;
        [CsvColumn(Name = "Resource Quantity")]
        public string resourcequantity;
        [CsvColumn(Name = "Donor's Guild")]
        public string donorsguild;
        [CsvColumn(Name = "Recipient Guild")]
        public string targetguild;

        public DonationDataLine(string charname, string account, string time, string item, string itemcount, string resource, string resourcequantity, string donorsguild, string targetguild)
        {
            this.charname = charname;
            this.account = account;
            this.time = time;
            this.item = item;
            this.itemcount = itemcount;
            this.resource = resource;
            this.resourcequantity = resourcequantity;
            this.donorsguild = donorsguild;
            this.targetguild = targetguild;
        }

        public DonationDataLine()
        {

        }
    }

    static class CSVReader
    {
        public static IEnumerable<DonationDataLine> ReadCSV(string path){
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            CsvContext cc = new CsvContext();

            IEnumerable<DonationDataLine> dataLines = cc.Read<DonationDataLine>(path, inputFileDescription);
            return dataLines;
        }
    }
}
