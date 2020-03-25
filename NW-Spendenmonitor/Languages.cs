using System;
using System.Collections.Generic;

namespace NW_Spendenmonitor
{
    static class Languages
    {
        public static string form_caption;
        public static string form_status;
        public static string form_importlanguage;
        public static string form_renamefile;
        public static string form_import;
        public static string form_export;
        public static string form_sqlhistory;
        public static string form_btn_generate_cmd;
        public static List<string> form_commands;
        public static List<string> form_importlanguages;
        public static List<string> form_uilanguages;
        public static string status_beingimported;
        public static string status_importof;
        public static string status_importfinished;
        public static string status_importentries;
        public static string message_renamefailed;
        public static string db_newdbcreated = "Neue Datenbank erfolgreich angelegt!";
        public static string db_connectionsuccess = "Erfolgreich zu bestehender Datenbank verbunden!";
        public static string export_success_clipboard;
        public static string export_success_file;
        public static string export_failed;


        public enum UILanguage { German, English};

        public static void SetLanguage(Main form, UILanguage language)
        {
            SetLanguage(form, language, false);
        }

        public static void SetLanguage(Main form, UILanguage language, bool init)
        {
            if ((language != ConfigClass.UILanguage) || init)
            {

                ConfigClass.UILanguage = language;
                
                form.SetConfig("UILanguage", ((int)language).ToString());    

                switch (language)
                {
                    //German
                    case UILanguage.German:
                        form_caption = "Neverwinter Spendenmonitor " + ConfigClass.VERSION;
                        form_status = "Status:";
                        form_importlanguage = "Spielsprache:";
                        form_renamefile = "Datei umbenennen";
                        form_import = "Import";
                        form_export = "Export";
                        form_sqlhistory = "SQL-Historie";
                        form_btn_generate_cmd = "Befehl erstellen";
                        form_commands = new List<string>
                        {
                            "Alle Ressourcen",
                            "Einfluss pro Account",
                            "Juwelen pro Account",
                            "Überschüssige Ausrüstung pro Account",
                            "Einfluss pro Tag",
                            "Juwelen pro Tag",
                            "Überschüssige Ausrüstung pro Tag",
                            "Belagerungsgutscheine pro Account"
                        };

                        form_importlanguages = new List<string>
                        {
                            "Englisch",
                            "Deutsch",
                            "Italienisch"
                        };

                        status_beingimported = " wird importiert, bitte warten!";
                        status_importof = "Import von ";
                        status_importfinished = " abgeschlossen, ";
                        status_importentries = " Einträge hinzugefügt!";

                        db_connectionsuccess = "Neue Datenbank erfolgreich angelegt!";
                        db_newdbcreated = "Erfolgreich zu bestehender Datenbank verbunden!";

                        message_renamefailed = "Es exisitiert bereits eine Datei, die die selben Datensätze beinhaltet. " + Environment.NewLine +
                            "Soll diese ALTE Datei umbenannt werden?" + Environment.NewLine +
                            "Bei \"Nein\" wird die aktuelle Datei nicht umbenannt!";

                        export_failed = "Export ist fehlgeschlagen! Ursprünglicher Fehler: ";
                        export_success_clipboard = "Daten erfolgreich in Zwischenablage kopiert!";
                        export_success_file = "Datei wurde erfolgreich gespeichert!";
                        break;

                    case UILanguage.English:
                        form_caption = "Neverwinter Donationmonitor " + ConfigClass.VERSION;
                        form_status = "Status:";
                        form_importlanguage = "Game language:";
                        form_renamefile = "Rename file";
                        form_import = "Import";
                        form_export = "Export";
                        form_sqlhistory = "SQL history";
                        form_btn_generate_cmd = "Generate command";
                        form_commands = new List<string>
                        {
                            "All ressources",
                            "Influence per account",
                            "Gems per account",
                            "Surplus equipment per account",
                            "Influence per day",
                            "Gems per day",
                            "Surplus equipment per day",
                            "Siege-vouchers per Account"
                        };

                        form_importlanguages = new List<string>
                        {
                            "English",
                            "German",
                            "Italian"
                        };

                        status_beingimported = " is being imported, please wait!";
                        status_importof = "Import of ";
                        status_importfinished = " done, ";
                        status_importentries = " entries added!";

                        db_connectionsuccess = "New database created!";
                        db_newdbcreated = "Successfully connected to existing database!";

                        message_renamefailed = "There already is a file ending on the same date. " + Environment.NewLine +
                            "Should this OLD file be renamed?" + Environment.NewLine +
                            "Selecting \"no\" will not rename the current file.";

                        export_failed = "Export failed! Original error message: ";
                        export_success_clipboard = "Data successfully exported to clipboard!";
                        export_success_file = "File successfully created!";
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected language = " + language);
                }
            }

        }

    }
}
