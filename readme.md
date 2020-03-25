# Neverwinter Online Donation Monitor

This is a tool for guildmasters in the online game Neverwinter. It aims to help them get an overview over donated material.

![Preview](https://raw.githubusercontent.com/chrisheib/NWO-Donation-Monitor/master/NW-Spendenmonitor/Grafik/preview.png)

## Features:
* Keep a database of all donations
* Evaluate the data and run statistics for gems, surplus equipment, influence, siege vouchers, and an overview of all ressources per day or account in a specific time range. 
* Supported User Interface languages: English, German
* Supported Game languages: English, German, Italian
* Run your own statistics using SQL

## Instructions:
1. Click on 'generate command' and select a path in which you want to store the file exported by Neverwinter. After you click 'save', the command is automatically copied into your clipboard. (Alternatively: Write down the command '/ExportGuildDonationLog E:\Neverwinterlogs\donation.csv' to somewhere and chose a filepath where Neverwinter does have access to.)
2. Open Neverwinter and paste the command into your chat (Ctrl+V).
If you've used the command correctly, a message should pop up in the system channel. Most of the time you need to execute the command twice for it to run without throwing an error.
3. Click 'Import'. Now choose the file you exported in step two. If you let 'Rename file' ticked, the file will be renamed with the date of the last entry, so you wont overwrite it the next time you export the log. Awesome for binding the export command to a key. If an error pops up, click 'Cancel' and repeat step one and two.
4. Enter a date and choose one of the reports from the dropdown menu. You should now see the magic in the field below :D

You can also make you own statements via SQL. Type them into the SQL box and hit 'Abfrage'. A good starting point is 'select * from input'.

Please remember neverwinter does only provide the last 500 entries in the log, so to have a complete coverage of all donations you must export the log every few days, depending on guild size and activity.

To reset or backup your data, copy or rename the nwmonitor.sqlite file thats in the directory of the exe file.

Unfortunately I don't own an expensive Code Signing Certificate, so you will probably get a warning when you start it for the first time. As much as its worth, you can skip the warning. If any kind of antivirus program triggers tho, do NOT run the program and report back to me please.

Feel free to try it out and make suggestions :D

Project page: [https://github.com/chrisheib/NWO-Donation-Monitor/](https://github.com/chrisheib/NWO-Donation-Monitor/)
