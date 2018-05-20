﻿using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    public static class ConfigClass
    {
        public static string VERSION = "v0.6";
        public static int ImportLanguage;
        public static Languages.UILanguage UILanguage;

        public static string GetConfig(SQLiteConnection dbConnection, string field)
        {
            return GetConfig(dbConnection, field, "");
        }

        public static string GetConfig(SQLiteConnection dbConnection, string field, string defaultValue)
        {
            string sqlCommand = "SELECT value from config where key like '" + field + "'";
            string result = DB.ReadValue(dbConnection, sqlCommand);
            if (result == "")
            {
                result = defaultValue;
            }
            return result;
        }

        public static void SetConfig(SQLiteConnection dbConnection, string field, string value)
        {
            string sqlCommand = "INSERT OR REPLACE INTO config (key, value) values ($field, $value)";
            SQLiteCommand command = new SQLiteCommand(sqlCommand);
            command.Parameters.AddWithValue("$field", field);
            command.Parameters.AddWithValue("$value", value);

            DB.Execute(dbConnection, command, false);
        }

        //public static void TestConfig(SQLiteConnection dbConnection)
        //{
        //    string test;
        //    SetConfig(dbConnection, "test1", "test1");
        //    SetConfig(dbConnection, "test2", "test2");
        //    SetConfig(dbConnection, "test3", "test3");

        //    test = GetConfig(dbConnection, "test1");
        //    test = GetConfig(dbConnection, "test2");
        //    test = GetConfig(dbConnection, "test3");

        //    SetConfig(dbConnection, "test1", "test4");
        //    SetConfig(dbConnection, "test2", "test5");
        //    SetConfig(dbConnection, "test3", "test6");

        //    test = GetConfig(dbConnection, "test1");
        //    test = GetConfig(dbConnection, "test2");
        //    test = GetConfig(dbConnection, "test3");
        //}
    }
}
