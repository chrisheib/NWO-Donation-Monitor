# Neverwinter Online Donation Monitor

This is a tool for guildmasters in the online game Neverwinter. It aims to help them get an overview over donated material.

![Preview](https://raw.githubusercontent.com/chrisheib/NWO-Donation-Monitor/master/NW-Spendenmonitor/Grafik/preview.png)

Instructions:
1. In Neverwinter execute the command '/ExportGuildDonationLog E:\Neverwinterlogs\donation.csv'. As path chose one which neverwinter has permission to write to. If you've use the command correctly, a message should pop up in the system channel. Sometimes you need to execute the command twice.
2. Start the tool and click 'Import'. Now choose the file you exported in step one. If you let 'Datei umbenennen' ticked, the file will be renamed with the date of the last entry, so you wont overwrite it the next time you export the log. Awesome for binding the export command to a key. If an error pops up, click 'Cancel' and repeat step one.
3. Enter a date and choose one of the reports from the dropdown menu. There currently are statistics for gems, surplus equipment, influence, vouchers, and an overview of all ressources. You should now see the magic in the field below :D

You can also make you own statements via SQL. Type them into the SQL box and hit 'Abfrage'. A good starting point is 'select * from input'.

Please remember neverwinter does only provide the last 500 entries in the log, so to have a complete coverage of all donations you must export the log every few days, depending on guild size and activity.

Unfortunately I don't know how to certify my program to microsoft, so you will probably get a warning when you start it for the first time. As much as its worth, you can skip the warning. If any kind of antivirus program triggers tho, do NOT run the program and report back to me please.

Feel free to try it out and make suggestions.
