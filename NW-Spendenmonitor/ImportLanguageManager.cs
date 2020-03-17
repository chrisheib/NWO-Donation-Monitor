using System;
using System.IO;
using System.Collections.Generic;

namespace NW_Spendenmonitor
{
    static class ImportLanguageManager
    {
        // copied from Matti Virkkunen: https://stackoverflow.com/a/2641383
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static void PrepareFileGerman(ref string path)
        {
            string line;
            List<string> rawLines = new List<string>();
            List<string> resultList = new List<string>();

            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                rawLines.Add(line);
            }
            file.Close();

            bool first = true;
            List<int> indexes;
            int from;
            int to;
            string edit;
            foreach (string fileLine in rawLines)
            {
                if (first)
                {
                    resultList.Add(fileLine);
                    first = false;
                }
                else
                {

                    indexes = fileLine.AllIndexesOf(",");
                    from = indexes[1];
                    to = indexes[3];
                    edit = fileLine;

                    //replace '<wbr>'
                    edit = edit.Replace("<wbr>", "");

                    //set date into quotes
                    edit = (edit.Insert(from + 1, "\"")).Insert(to + 1, "\"");
                    resultList.Add(edit);
                }
            }

            string filename = Path.GetFileNameWithoutExtension(path);
            string fileextension = Path.GetExtension(path);
            filename = filename + " formatted" + fileextension;
            path = Path.GetDirectoryName(path) + "\\" + filename;
            
            TextWriter tw = new StreamWriter(path);
            foreach (string s in resultList)
            {
                tw.WriteLine(s);
            }
            tw.Close();
        }
    }
}
