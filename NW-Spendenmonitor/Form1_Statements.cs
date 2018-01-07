using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NW_Spendenmonitor
{
    public partial class Form1
    {


        private void Statement_CountInfluence()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Einfluss desc";
            StatementToGrid(statement, false);
        }

        private void Statement_CountVouchers()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where item like '%voucher%'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Gutscheinanzahl desc";
            StatementToGrid(statement, false);
        }

        private void Statement_CountGems()
        {
            GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Juwelen desc";
            StatementToGrid(statement, false);

        }
    }
}
