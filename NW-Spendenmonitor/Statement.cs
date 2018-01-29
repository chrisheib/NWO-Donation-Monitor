using System.Windows.Forms;

namespace NW_Spendenmonitor
{
    public class Statement
    {

        public static void RunStatement(Form1 form, int selection)
        {
            switch (selection)
            {
                case 0:
                    CountAllResourceTypes(form);
                    break;
                case 1:
                    CountInfluencePerAccount(form);
                    break;
                case 2:
                    CountGemsPerAccount(form);
                    break;
                case 3:
                    CountSurplusPerAccount(form);
                    break;
                case 4:
                    CountInfluencePerDay(form);
                    break;
                case 5:
                    CountGemsPerDay(form);
                    break;
                case 6:
                    CountSurplusPerDay(form);
                    break;
                case 7:
                    CountVouchersPerAccount(form);
                    break;
                default:
                    break;
            }
        }

        public static void SetStatementCollection(ComboBox cb)
        {
            cb.Items.Add("Alle Ressourcen von ... bis");
            cb.Items.Add("Einfluss pro Account von...bis");
            cb.Items.Add("Juwelen pro Account von ... bis");
            cb.Items.Add("Überfl. Ausr. pro Account von ... bis");
            cb.Items.Add("Einfluss pro Tag von...bis");
            cb.Items.Add("Juwelen pro Tag von ... bis");
            cb.Items.Add("Überfl. Ausr. pro Tag von ... bis");
            cb.Items.Add("Gutscheine pro Account von ... bis");
        }

        //0
        public static void CountAllResourceTypes(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select resource, sum(resourcequantity) Ressourcenanzahl from input where " +
                "time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by resource order by Ressourcenanzahl desc";
            form.StatementToGrid(statement, false);
        }

        //1
        public static void CountInfluencePerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " or resource like 'Einfluss' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Einfluss desc";
            form.StatementToGrid(statement, false);
        }

        //2
        public static void CountGemsPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " or resource like 'Juwelen' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Juwelen desc";
            form.StatementToGrid(statement, false);
        }

        //3
        public static void CountSurplusPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) 'Überschüssige Ausrüstung' from input where resource like 'Surplus Equipment'" +
                " or resource like 'Überschüssige Ausrüstung' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by sum(resourcequantity) desc";
            form.StatementToGrid(statement, false);
        }

        //4
        public static void CountInfluencePerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Einfluss from input where resource like 'influence'" +
                " or resource like 'Einfluss' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by date(time) order by Tag desc";
            form.StatementToGrid(statement, false);
        }

        //5
        public static void CountGemsPerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Juwelen from input where resource like 'gems'" +
                " or resource like 'Juwelen' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by date(time) order by Tag desc";
            form.StatementToGrid(statement, false);
        }

        //6
        public static void CountSurplusPerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) 'Überschüssige Ausrüstung' from input where resource like 'Surplus Equipment'" +
                " or resource like 'Überschüssige Ausrüstung' and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by date(time) order by Tag desc";
            form.StatementToGrid(statement, false);
        }

        //7
        public static void CountVouchersPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where item like '%voucher%'" +
                " and time >= '" + dateFrom + "' and time <= '" + dateTo + "' group by account order by Gutscheinanzahl desc";
            form.StatementToGrid(statement, false);
        }
    }
}
