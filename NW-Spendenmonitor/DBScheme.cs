using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    static class DBScheme
    {
        static public bool InitScheme(SQLiteConnection dbConnection)
        {
            bool result = false;
            string statement;

            statement = "CREATE TABLE commands (id INTEGER PRIMARY KEY AUTOINCREMENT, command TEXT, date DATETIME)";
            DB.Execute(dbConnection, statement, false);

            statement = "CREATE TABLE input ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `charname` TEXT, `account` TEXT, `time` TEXT, `item` TEXT, `itemcount` TEXT, `resource` TEXT, `resourcequantity` TEXT, `donorsguild` TEXT, `targetguild` TEXT )";
            DB.Execute(dbConnection, statement, false);

            return result;
        } 
    }
}