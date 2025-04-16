# GalaxyBackupV3

Utility for creating automated Backups of Aveva System Platform Galaxy (Wonderware Application Server)

GalaxyBackup v4.1.1.0

1. execute the Backup Tool only on the GR Node.

2. start the Commandline Tool with 4 Parameters (Edit the startBackup.bat file)
	GalaxyBackup.exe <GalaxyName> <Foldername> <BackupsOnline> <isDevLicense>
		a. Galaxyname: Name of the Galaxy
		b. Path of Foldername where the Backup will be created, if the Path contains space put it in quotation marks
		c. How many Backups you wish to have online, for example 14 means all backups older than 14 days will be automatically deleted (if you put 0 ALL YOUR BACKUPS will be deleted)
		d. If you have a License with more than 1 Dev Count set this Parameter to 1

3. The Commandline Tool must run as Administrator

4. the Backup will be created and the cab File appear in the Folder

5. These files have to stay in the GR node, and also is in the GR to create the Scheduled Task


Example: "C:\Program Files (x86)\ArchestrA\aaBackup\GalaxyBackup.exe" "Coffee_StandardSolution" "\\chorrl0043\PATA\Coffee Standard Solution - Development\00 GalaxyBackup\IAS 2012 R2" 90 1


Tested on following Versions:
Application Server 2017 U3, 2020 and 2023 R2

Questions contact oliver.varosanec@aveva.com
