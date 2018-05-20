using System.Data.SQLite;
using System.IO;
using System;

namespace NW_Spendenmonitor
{

    class DB
    {
        public static SQLiteConnection OpenSQLConnection(out string result)
        {
            var newDB = false;
            if (!File.Exists("nwmonitor.sqlite"))
            {
                SQLiteConnection.CreateFile("nwmonitor.sqlite");
                newDB = true;
            }

            var m_dbConnection = new SQLiteConnection("Data Source=nwmonitor.sqlite;Version=3;");
            m_dbConnection.Open();

            DBScheme.InitScheme(m_dbConnection);

            if (newDB)
            {
                result = Languages.db_newdbcreated;
            }
            else
            {
                result = Languages.db_connectionsuccess;
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
                query = null;
            }

            return result;

        }

        public static string ReadValue(SQLiteConnection connect, string sql)
        {
            if (Select(connect, sql, out SQLiteDataReader reader, false))
            {
                try
                {
                    //reader.Read();
                    reader.Read();
                    return reader.GetString(0);
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static bool Execute(SQLiteConnection connect, string sql)
        {
            return Execute(connect, sql, true);
        }

        public static bool Execute(SQLiteConnection connect, string sql, bool logging)
        {
            var command = new SQLiteCommand(sql, connect);
            return Execute(connect, command, logging);
        }

        public static bool Execute(SQLiteConnection connect, SQLiteCommand command)
        {
            return Execute(connect, command, true);
        }

        public static bool Execute(SQLiteConnection connect, SQLiteCommand command, bool logging)
        {
            if (logging)
            {
                LogCommand(connect, command.CommandText);
            }

            bool result = false;
            try
            {
                command.Connection = connect;
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
            //string sqlintern = "insert into commands (command, date) values ('" + sql + "', '" + sqlFormattedDate + "')";

            string sqlintern = "insert into commands (command, date) values ($sql, $date)";
            SQLiteCommand command = new SQLiteCommand(sqlintern);
            command.Parameters.AddWithValue("$sql", sql);
            command.Parameters.AddWithValue("$date", sqlFormattedDate);

            Execute(connect, command, false);
        }
    }
}