using System.Data.SQLite;
using System.IO;
using System;

namespace NW_Spendenmonitor
{
    class DB
    {
        public static bool ExceptionError = false;

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
            return Select(connect, new SQLiteCommand(sql), out query);
        }

        public static bool Select(SQLiteConnection connect, SQLiteCommand command, out SQLiteDataReader query)
        {
            return Select(connect, command, out query, true);
        }

        public static bool Select(SQLiteConnection connect, string sql, out SQLiteDataReader query, bool logging)
        {
            return Select(connect, new SQLiteCommand(sql), out query, logging);
        }

        public static bool Select(SQLiteConnection connect, SQLiteCommand command, out SQLiteDataReader query, bool logging)
        {
            if (logging)
            {
                LogCommand(connect, command);
            }

            bool result = false;

            try
            {
                command.Connection = connect;
                query = command.ExecuteReader();
                result = true;
            }
            catch (Exception e)
            {
                LogException(connect, e);
                query = null;
            }

            return result;

        }

        public static string ReadValue(SQLiteConnection connect, string sql)
        {
            return ReadValue(connect, new SQLiteCommand(sql));
        }

        public static string ReadValue(SQLiteConnection connect, SQLiteCommand command)
        {
            if (Select(connect, command, out SQLiteDataReader reader, false))
            {
                try
                {
                    reader.Read();
                    return reader.GetString(0);
                }
                catch (Exception e)
                {
                    LogException(connect, e);
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
                LogCommand(connect, command);
            }

            bool result = false;
            try
            {
                command.Connection = connect;
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                LogException(connect, e);
            }
            return result;

        }

        public static void LogCommand(SQLiteConnection connect, SQLiteCommand commandIn)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sql = commandIn.CommandText;

            foreach (SQLiteParameter p in commandIn.Parameters)
            {
                sql = sql.Replace(p.ParameterName, "'" + p.Value.ToString() + "'");
            }

            string sqlintern = "insert into commands (command, date) values ($sql, $date)";
            SQLiteCommand command = new SQLiteCommand(sqlintern);
            command.Parameters.AddWithValue("$sql", sql);
            command.Parameters.AddWithValue("$date", sqlFormattedDate);

            Execute(connect, command, false);
        }

        public static void LogException(SQLiteConnection connect, Exception exception)
        {
            if (!ExceptionError)
            {

                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string sql = "INSERT INTO exceptions (exception, date) values ($exception, $date)";
                SQLiteCommand command = new SQLiteCommand(sql);
                command.Parameters.AddWithValue("$exception", exception.Message);
                command.Parameters.AddWithValue("$date", sqlFormattedDate);

                ExceptionError = true;
                try
                {
                    Execute(connect, command, false);
                    ExceptionError = true;
                }
                catch (Exception e)
                {
                    LogException(connect, e);
                }
            }
        }
    }
}