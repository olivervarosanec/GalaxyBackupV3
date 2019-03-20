namespace aaGalaxyBackup
{
    using ArchestrA.GRAccess;
    using System;
    using System.Diagnostics;

    public class ArchestraBackup
    {
        private string filePath;
        private string galaxyName;
        private GRAccessApp myGRAccess;

        public ArchestraBackup(string GalaxyName, string FolderName)
        {
            if (!(GalaxyName != "") && !(FolderName != ""))
            {
                throw new Exception("invalid Arguments in Constructor");
            }
            this.galaxyName = GalaxyName;
            this.filePath = FolderName;
        }

        public string BackupGalaxy()
        {
            bool flag = false;
            try
            {
                string machineName = Environment.MachineName;
                this.myGRAccess = new GRAccessAppClass();
                IGalaxies galaxies = this.myGRAccess.QueryGalaxies(machineName);
                int id = Process.GetCurrentProcess().Id;
                IGalaxy galaxy = galaxies[this.galaxyName];
                if (galaxy != null)
                {
                    galaxy.Backup(id, this.filePath, machineName, this.galaxyName);
                    //ICommandResult commandResult = galaxy.CommandResult;
                    //if (!commandResult.Successful)
                    //{
                        //return ("cmd.Successful: " + commandResult.Text + " - " + commandResult.CustomMessage);
                    //}
                }
                else
                {
                    flag = true;
                    throw new Exception("Galaxy " + this.galaxyName + " does not exist!");
                }
            }
            catch (Exception exception)
            {
                flag = true;
                throw new Exception("Backup Galaxy Failed: " + exception.Message + " - " + exception.StackTrace);
            }
            if (!flag)
            {
                return "BackUP Successfull";
            }
            return "BackUP Failed";
        }
    }
}

