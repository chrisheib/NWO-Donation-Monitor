using System.Data.SQLite;
using System.IO;
using System;

namespace NW_Spendenmonitor
{

    class DB
    {

        public static SQLiteConnection OpenSQLConnection()
        {
            var newDB = false;
            if (!File.Exists("nwmonitor.sqlite"))
            {
                SQLiteConnection.CreateFile("nwmonitor.sqlite");
                newDB = true;
            }

            var m_dbConnection = new SQLiteConnection("Data Source=nwmonitor.sqlite;Version=3;");
            m_dbConnection.Open();

            if (newDB)
            {
                DBScheme.InitScheme(m_dbConnection);
            }

            return m_dbConnection;
        }

        public static bool Select(SQLiteConnection connect, string sql, out SQLiteDataReader query)
        {
            bool result = Select(connect, sql, out query, true);
            return result;
        }

        public static bool Select(SQLiteConnection connect, string sql, out SQLiteDataReader query, bool logging)
        {
            if (logging)
            {
                LogCommand(connect, sql);
            }

            bool result = false;

            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, connect);
                query = command.ExecuteReader();
                result = true;
            }
            catch
            {
                query = new SQLiteCommand("select 'hi'", connect).ExecuteReader();
            }

            return result;

        }

        public static bool Execute(SQLiteConnection connect, string sql)
        {
            return Execute(connect, sql, true);
        }

        public static bool Execute(SQLiteConnection connect, string sql, bool logging)
        {
            if (logging)
            {
                LogCommand(connect, sql);
            }

            bool result = false; 

            var command = new SQLiteCommand(sql, connect);
            try
            {
                command.ExecuteNonQuery();
                result = true;
            }
            catch
            {
                
            }
            return result;
        }

        public static void LogCommand(SQLiteConnection connect, string sql)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlintern = "insert into commands (command, date) values ('" + sql + "', '" + sqlFormattedDate + "')";
            Execute(connect, sqlintern, false);
        }
    }
}