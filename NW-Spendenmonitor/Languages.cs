using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW_Spendenmonitor
{
    public static class Languages
    {
        public static string form_caption;
        public static string form_status;
        public static string form_importlanguage;
        public static string form_renamefile;
        public static string form_import;
        public static string form_sqlhistory;
        public static List<string> form_commands;
        public static List<string> form_importlanguages;
        public static string status_beingimported;
        public static string status_importof;
        public static string status_importfinished;
        public static string status_importentries;
        public static string message_renamefailed;

        public enum Language { German, English};

        public static void SetLanguage(Language language)
        {
            ConfigClass.UILanguage = language;
            switch (language)
            {
                //German
                case Language.German:
                    form_caption = "Neverwinter Spendenmonitor";
                    form_status = "Status:";
                    form_importlanguage = "Spielsprache:";
                    form_renamefile = "Datei umbenennen";
                    form_import = "Import";
                    form_sqlhistory = "SQL-Historie";
                    form_commands = new List<string>
                    {
                        "Alle Ressourcen von ... bis",
                        "Einfluss pro Account von...bis",
                        "Juwelen pro Account von ... bis",
                        "Überfl. Ausr. pro Account von ... bis",
                        "Einfluss pro Tag von...bis",
                        "Juwelen pro Tag von ... bis",
                        "Überfl. Ausr. pro Tag von ... bis",
                        "Gutscheine pro Account von ... bis"
                    };

                    form_importlanguages = new List<string>
                    {
                        "Englisch",
                        "Deutsch"
                    };

                    status_beingimported = " wird importiert, bitte warten!";
                    status_importof = "Import von ";
                    status_importfinished = " abgeschlossen, ";
                    status_importentries = " Einträge hinzugefügt!";

                    message_renamefailed = "Es exisitiert bereits eine Datei, die die selben Datensätze beinhaltet. " + Environment.NewLine +
                        "Soll diese ALTE Datei umbenannt werden?" + Environment.NewLine +
                        "Bei \"Nein\" wird die aktuelle Datei nicht umbenannt!";
                    break;

                case Language.English:
                    break;

                default:
                    break;
            }
        }

    }
}
