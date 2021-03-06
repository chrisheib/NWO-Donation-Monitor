﻿using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    static class DBScheme
    {
        static public bool InitScheme(SQLiteConnection dbConnection)
        {
            bool result = false;
            string statement;

            statement = "CREATE TABLE commands (id INTEGER PRIMARY KEY AUTOINCREMENT, command TEXT, date DATETIME)";
            AddTable(dbConnection, "commands", statement);

            statement = "CREATE TABLE input ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `charname` TEXT, `account` TEXT, `time` TEXT, `item` TEXT, `itemcount` TEXT, `resource` TEXT, `resourcequantity` TEXT, `donorsguild` TEXT, `targetguild` TEXT )";
            AddTable(dbConnection, "input", statement);

            statement = "CREATE TABLE config ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `key` TEXT NOT NULL UNIQUE, `value` TEXT)";
            AddTable(dbConnection, "config", statement);

            statement = "CREATE TABLE exceptions (id INTEGER PRIMARY KEY AUTOINCREMENT, exception TEXT, date DATETIME)";
            AddTable(dbConnection, "exceptions", statement);

            return result;
        } 

        static public void AddTable(SQLiteConnection dbConnection, string tablename, string createString)
        {
            string statement = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = $tablename";
            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$tablename", tablename);

            string table = DB.ReadValue(dbConnection, command);
            if (table == tablename) 
            {
                //TODO: Add table altering
            }
            else
            {
                DB.Execute(dbConnection, createString, false);
            }
        }
    }
}