﻿using System.Data.SQLite;

namespace NW_Spendenmonitor
{
    public class Statement
    {
        public enum Resource
        {
            Stone, Food, Wood, Metal, DungeonShard, HeroicShard,
            AdventurerShard, ConquerorShard, Glory, Labor, Gems, SurplusEquipment,
            AstralDiamondChests, Gold, GeyTrinkets, DarkGifts, FrozenTreasures,
            TreasuresOfTyranny, Influence
        };

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

            form.SetConfig("LastStatistic", selection.ToString());
        }

        //0
        public static void CountAllResourceTypes(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select resource, sum(resourcequantity) Ressourcenanzahl from input where " +
                "(time >= $dateFrom and time <= $dateTo) group by resource order by Ressourcenanzahl desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //1
        public static void CountInfluencePerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Einfluss from input where (resource like 'influence'" +
                " or resource like 'Einfluss') and (time >= $dateFrom and time <= $dateTo) group by account order by Einfluss desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //2
        public static void CountGemsPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) Juwelen from input where (resource like 'gems'" +
                " or resource like 'Juwelen') and (time >= $dateFrom and time <= $dateTo) group by account order by Juwelen desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //3
        public static void CountSurplusPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(resourcequantity) 'Überschüssige Ausrüstung' from input where (resource like 'Surplus Equipment'" +
                " or resource like 'Überschüssige Ausrüstung') and (time >= $dateFrom and time <= $dateTo) group by account order by sum(resourcequantity) desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //4
        public static void CountInfluencePerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Einfluss from input where (resource like 'influence'" +
                " or resource like 'Einfluss') and (time >= $dateFrom and time <= $dateTo) group by date(time) order by Tag desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //5
        public static void CountGemsPerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) Juwelen from input where (resource like 'gems'" +
                " or resource like 'Juwelen') and (time >= $dateFrom and time <= $dateTo) group by date(time) order by Tag desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, true);
        }

        //6
        public static void CountSurplusPerDay(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select date(time) Tag, sum(resourcequantity) 'Überschüssige Ausrüstung' from input where (resource like 'Surplus Equipment'" +
                " or resource like 'Überschüssige Ausrüstung') and (time >= $dateFrom and time <= $dateTo) group by date(time) order by Tag desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //7
        public static void CountVouchersPerAccount(Form1 form)
        {
            form.GetFromToDates(out string dateFrom, out string dateTo);
            string statement = "select charname, account, sum(itemcount) Gutscheinanzahl from input where (item like '%voucher%')" +
                " and (time >= $dateFrom and time <= $dateTo) group by account order by Gutscheinanzahl desc";

            SQLiteCommand command = new SQLiteCommand(statement);
            command.Parameters.AddWithValue("$dateFrom", dateFrom);
            command.Parameters.AddWithValue("$dateTo", dateTo);

            form.StatementToGrid(command, false);
        }

        //public static SQLiteCommand GetResourcePerDay(Resource r)
        //{
        //    string statement = "select charname, account, sum(resourcequantity) Einfluss from input where (resource like $resource)" +
        //"and (time >= $dateFrom and time <= $dateTo) group by account order by Einfluss desc";
        //
        //} 
        //string statement = "select date(time) Tag, sum(resourcequantity) 'Überschüssige Ausrüstung' from input where (resource like 'Surplus Equipment'" +
        //    " or resource like 'Überschüssige Ausrüstung') and (time >= $dateFrom and time <= $dateTo) group by date(time) order by Tag desc";

    }
}
