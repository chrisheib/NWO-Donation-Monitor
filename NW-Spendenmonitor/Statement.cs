namespace NW_Spendenmonitor
{
    public class Statement
    {

        public static void RunStatement(Form1 form, int selection)
        {
            switch (selection)
            {
                case 0:
                    CountVouchersPerAccount(form);
                    break;
                case 1:
                    CountInfluencePerAccount(form);
                    break;
                case 2:
                    CountGemsPerAccount(form);
                    break;
                case 3:
                    CountInfluencePerDay(form);
                    break;
                case 4:
                    CountGemsPerDay(form);
                    break;
                default:
                    break;
            }
        }

        //0
        public static void CountVouchersPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where item like '%voucher%'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Gutscheinanzahl desc";
            form.StatementToGrid(statement, false);
        }

        //1
        public static void CountInfluencePerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Einfluss desc";
            form.StatementToGrid(statement, false);
        }

        //2
        public static void CountGemsPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Juwelen desc";
            form.StatementToGrid(statement, false);
        }

        //3
        public static void CountInfluencePerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by date(time) order by Tag desc";
            form.StatementToGrid(statement, false);
        }

        //4
        public static void CountGemsPerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by date(time) order by Tag desc";
            form.StatementToGrid(statement, false);
        }
    }
}
