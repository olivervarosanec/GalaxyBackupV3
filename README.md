# GalaxyBackupV3

Utility for creating automated backups of **AVEVA System Platform Galaxy (Wonderware Application Server)**.

## Version
**GalaxyBackup v4.1.1.0**

## Requirements
- Execute the backup tool only on the GR Node.
- The command-line tool **must** be run as Administrator.

## Usage
Start the command-line tool with four parameters (edit the `startBackup.bat` file):

```
GalaxyBackup.exe <GalaxyName> <BackupPath> <BackupRetentionDays> <DevCountLicense>
```

- **GalaxyName:** Name of the Galaxy
- **BackupPath:** Path of the folder where the backup will be created. Enclose the path in quotation marks if it contains spaces.
- **BackupRetentionDays:** Number of backups you wish to keep. Backups older than this number of days will be automatically deleted. *(Important: Setting this value to 0 will delete ALL YOUR BACKUPS.)*
- **DevCountLicense:** Set this parameter to `1` if you have a license with more than one Dev Count.

### Example
```batch
"C:\Program Files (x86)\ArchestrA\aaBackup\GalaxyBackup.exe" "Coffee_StandardSolution" "\\chorrl0043\PATA\Coffee Standard Solution - Development\00 GalaxyBackup\IAS 2012 R2" 90 1
```

## Backup Output
The backup process generates `.cab` files that will be placed in the specified backup folder.

- Ensure these files remain on the GR Node.
- Schedule automated backups via Windows Scheduled Task on the GR Node.

## Tested Versions
- Application Server 2017 U3
- Application Server 2020
- Application Server 2023 R2

## Support
For questions or support, please contact:

📧 **oliver.varosanec@aveva.com**

