namespace GalaxyBackup
{
    using aaGalaxyBackup;
    using System;
    using LoggerDLL;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;

    internal class Program
    {

        private static void Main(string[] args)
        {
            bool errorflag = false;
            bool flag = false;
            if ((args.Length == 4) && Directory.Exists(args[1]))
            {
                try
                {
                    string str = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    double num = -Convert.ToDouble(args[2]);
                    DateTime time = DateTime.Now.AddDays(num);
                    FileInfo[] files = new DirectoryInfo(args[1]).GetFiles();
                    a2logger.LogSetIdentityName("GalaxyBackupUtility");
                    a2logger.LogInfo("GalaxyBackup Utility Version: " + str);

                    for (int i = 0; i < files.Length; i++)
                    {

                        if ((files[i].LastWriteTime < time) && (files[i].Extension == ".cab"))
                        {
                            File.Delete(files[i].FullName);
                            a2logger.LogInfo("...deleting Backups older then " + args[2] + "days ---> " + files[i].FullName);
                        }
                    }
                }
                catch (Exception exception)
                {
                    errorflag = true;
                    a2logger.LogInfo(exception.Message + Environment.NewLine);
                }
            }
            if ((args.Length == 4) && (Directory.Exists(args[1])) && (args[1] != ""))
            {
                try
                {
                    Int32 devLicInstalled = 0;
                    if (args.Length > 3)
                    {
                        devLicInstalled = Convert.ToInt32(args[3]);
                    }

                    if (CheckIfAProcessIsRunning("aaIDE") && devLicInstalled == 0)
                    {
                        throw new Exception("aaIDE.exe is running, Backup will be skipped");
                    }

                    string FileName = args[1] + @"\" + args[0] + "_" + DateTime.Now.ToString("yyyy_MM_dd_hhmmss");
                    a2logger.LogInfo("...starting automatic galaxy Backup");

                    Thread thread = new Thread(delegate (object unused) {
                        if (DoBackUP(args[0], FileName))
                        {
                            errorflag = true;
                        }
                    });
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    thread.Start();
                    a2logger.LogInfo("creating Backup ... Please wait.....");
                    while (thread.IsAlive)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    flag = true;
                    stopwatch.Stop();
                    if (!(errorflag || !flag))
                    {
                        Console.WriteLine(Environment.NewLine);
                        a2logger.LogInfo("Backup Completed at " + FileName + " in " + stopwatch.Elapsed.ToString());
                    }
                    else
                    {
                        Console.WriteLine(Environment.NewLine);
                        a2logger.LogError("BackUp Failed.....");
                    }
                }
                catch (Exception exception2)
                {
                    errorflag = true;
                    a2logger.LogError(exception2.Message + exception2.StackTrace);
                }
            }
            else
            {
                a2logger.LogError("Invalid Arguments or Directory does not exist...");
            }
        }



        public static bool CheckIfAProcessIsRunning(string processname)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains(processname))
                {
                    return true;
                }
            }
            return false;
        }



        public static bool DoBackUP(string galaxyName, string filePath)
        {
            try
            {
                new ArchestraBackup(galaxyName, filePath).BackupGalaxy();
                return false;
            }
            catch (Exception exception)
            {
                a2logger.LogError("BackUp Failed with " + exception.Message);
                return true;
            }
        }


    }
}

