using System.Collections.Generic;
using System;
using System.Linq;
using LINQtoCSV; //http://www.aspnetperformance.com/post/LINQ-to-CSV-library.aspx

namespace NW_Spendenmonitor
{
    class DonationDataLine
    {
        string charname;
        [CsvColumn(Name = "Character Name")]
        public string Charname
        {
            get { return charname; }
            set { charname = ClearApostrophes(value); }
        }

        string account;
        [CsvColumn(Name = "Account Handle")]
        public string Account
        {
            get { return account; }
            set { account = ClearApostrophes(value); }
        }

        private string time;
        [CsvColumn(Name = "Time")]
        public string Time
        {
            get { return time; }
            set { time = FormatTime(value); }
        }

        string item;
        [CsvColumn(Name = "Item")]
        public string Item
        {
            get { return item; }
            set { item = ClearApostrophes(value); }
        }

        string itemcount;
        [CsvColumn(Name = "Item Count")]
        public string Itemcount
        {
            get { return itemcount; }
            set { itemcount = ClearApostrophes(value); }
        }

        string resource;
        [CsvColumn(Name = "Resource")]
        public string Resource
        {
            get { return resource; }
            set { resource = ClearApostrophes(value); }
        }

        string resourcequantity;
        [CsvColumn(Name = "Resource Quantity")]
        public string Resourcequantity
        {
            get { return resourcequantity; }
            set { resourcequantity = ClearApostrophes(value); }
        }

        string donorsguild;
        [CsvColumn(Name = "Donor's Guild")]
        public string Donorsguild
        {
            get { return donorsguild; }
            set { donorsguild = ClearApostrophes(value); }
        }

        string targetguild;
        [CsvColumn(Name = "Recipient Guild")]
        public string Targetguild
        {
            get { return targetguild; }
            set { targetguild = ClearApostrophes(value); }
        }

        public DonationDataLine(string charname, string account, string time, string item, string itemcount, string resource, string resourcequantity, string donorsguild, string targetguild)
        {
            Charname = charname;
            Account = account;
            Time = time;
            Item = item;
            Itemcount = itemcount;
            Resource = resource;
            Resourcequantity = resourcequantity;
            Donorsguild = donorsguild;
            Targetguild = targetguild;
        }

        public DonationDataLine()
        {

        }

        private string ClearApostrophes(string input)
        {
            return input.Replace("'", "");
        }

        private string FormatTime(string csvTime)
        {
            string formattedDateTime = "";
            string year = "";
            string month = "";
            string day = "";
            string formattedTime = "";
            switch (ConfigClass.ImportLanguage)
            {
                case 0:
                    //8/26/2017 1:23:00 AM
                    string[] fullDateTime = csvTime.Split(' ');
                    string[] fullDate = fullDateTime[0].Split('/');
                    string fullTime = fullDateTime[1] + ' ' + fullDateTime[2];

                    day = fullDate[1].PadLeft(2, '0');
                    month = fullDate[0].PadLeft(2, '0');
                    year = fullDate[2];

                    formattedTime = DateTime.Parse(fullTime).ToLongTimeString();
                    break;

                case 1:
                    //28.1.2018, 22:13:30
                    fullDateTime = csvTime.Split(',');
                    fullDate = fullDateTime[0].Split('.');
                    fullTime = fullDateTime[1];

                    day = fullDate[0].PadLeft(2, '0');
                    month = fullDate[1].PadLeft(2, '0');
                    year = fullDate[2];

                    formattedTime = DateTime.Parse(fullTime).ToLongTimeString();
                    break;

                default:
                    break;
            }
            formattedDateTime = year + '-' + month + '-' + day + ' ' + formattedTime;
            return formattedDateTime;
        }
    }

    static class CSVReader
    {
        public static List<DonationDataLine> ReadCSV(string path){
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            CsvContext cc = new CsvContext();

            List<DonationDataLine> dataLines = cc.Read<DonationDataLine>(path, inputFileDescription).ToList();

            return dataLines;
        }
    }
}
