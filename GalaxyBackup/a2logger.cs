namespace LoggerDLL
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    internal class a2logger
    {
        private static bool LoggerDllLoaded;
        private static int mhIdentity;

        private static int a2Logger()
        {
            LoggerDllLoaded = false;
            mhIdentity = 0;
            return 0;
        }

        [DllImport("LoggerDLL.dll", EntryPoint="GETLOGGERSTATS", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int GetLoggerStats([MarshalAs(UnmanagedType.LPWStr)] string HostName, ref int ErrorCount, ref long ftLastError, ref int WarningCount, ref long ftLastWarning);
        private static bool Initialize()
        {
            if (mhIdentity != 0)
            {
                return true;
            }
            InitLoggerDll();
            RegisterLoggerClient(ref mhIdentity);
            if (mhIdentity != 0)
            {
                SetIdentityName(mhIdentity, Assembly.GetExecutingAssembly().GetName().Name);
                return true;
            }
            return false;
        }

        private static void InitLoggerDll()
        {
            if (!LoggerDllLoaded)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\ArchestrA\Framework\Logger", false);
                if (key != null)
                {
                    string str = Convert.ToString(key.GetValue("InstallPath", ""));
                    if (str.Length > 0)
                    {
                        LoadLibraryW(Path.Combine(str, "LoggerDll.dll"));
                    }
                }
                LoggerDllLoaded = true;
            }
        }

        [DllImport("LoggerDLL.dll", EntryPoint="LOGCONNECTION", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogConnection(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGCTORDTOR", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogCtorDtor(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGCUSTOM2", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogCustom(int hIdentity, int nCustomFlag, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGENTRYEXIT", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogEntryExit(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGERROR", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogError(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGINFO", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogInfo(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGREFCOUNT", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogRefCount(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGSQL", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogSQL(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGSTARTSTOP", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogStartStop(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGTHREADSTARTSTOP", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogThreadStartStop(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGTRACE", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogTrace(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("LoggerDLL.dll", EntryPoint="LOGWARNING", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern void InternalLogWarning(int hIdentity, [MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibraryW([MarshalAs(UnmanagedType.LPWStr)] string lpMdoule);
        public static void LogConnection(string message)
        {
            if (Initialize())
            {
                InternalLogConnection(mhIdentity, message);
            }
        }

        public static void LogCtorDtor(string message)
        {
            if (Initialize())
            {
                InternalLogCtorDtor(mhIdentity, message);
            }
        }

        public static void LogCustom(int cookie, string message)
        {
            if (Initialize())
            {
                InternalLogCustom(mhIdentity, cookie, message);
            }
        }

        public static void LogEntryExit(string message)
        {
            if (Initialize())
            {
                InternalLogEntryExit(mhIdentity, message);
            }
        }

        public static void LogError(string message)
        {
            if (Initialize())
            {
                InternalLogError(mhIdentity, message);
            }
        }

        public static void LogInfo(string message)
        {
            if (Initialize())
            {
                InternalLogInfo(mhIdentity, message);
            }
        }

        public static void LogRefCount(string message)
        {
            if (Initialize())
            {
                InternalLogRefCount(mhIdentity, message);
            }
        }

        public static int LogRegisterCustomFlag(string strFlagName)
        {
            if (Initialize())
            {
                return RegisterLogFlag(mhIdentity, 11, strFlagName);
            }
            return 0;
        }

        public static int LogRegisterCustomFlagEx(string strFlagName, int nDefaultVal)
        {
            if (Initialize())
            {
                return RegisterLogFlagEx(mhIdentity, 11, strFlagName, nDefaultVal);
            }
            return 0;
        }

        public static void LogSetIdentityName(string strIdentityName)
        {
            if (Initialize())
            {
                SetIdentityName(mhIdentity, strIdentityName);
            }
        }

        public static void LogSQL(string message)
        {
            if (Initialize())
            {
                InternalLogSQL(mhIdentity, message);
            }
        }

        public static void LogStartStop(string message)
        {
            if (Initialize())
            {
                InternalLogStartStop(mhIdentity, message);
            }
        }

        public static void LogThreadStartStop(string message)
        {
            if (Initialize())
            {
                InternalLogThreadStartStop(mhIdentity, message);
            }
        }

        public static void LogTrace(string message)
        {
            if (Initialize())
            {
                InternalLogTrace(mhIdentity, message);
            }
        }

        public static void LogWarning(string message)
        {
            if (Initialize())
            {
                InternalLogWarning(mhIdentity, message);
            }
        }

        [DllImport("LoggerDLL.dll", EntryPoint="REGISTERLOGFLAG", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int RegisterLogFlag(int hIdentity, int nCustomFlag, [MarshalAs(UnmanagedType.LPWStr)] string strFlag);
        [DllImport("LoggerDLL.dll", EntryPoint="REGISTERLOGFLAGEX", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int RegisterLogFlagEx(int hIdentity, int nCustomFlag, [MarshalAs(UnmanagedType.LPWStr)] string strFlag, int nDefaultVal);
        [DllImport("LoggerDLL.dll", EntryPoint="REGISTERLOGGERCLIENT", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int RegisterLoggerClient(ref int hIdentity);
        [DllImport("LoggerDLL.dll", EntryPoint="SETIDENTITYNAME", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int SetIdentityName(int hIdentity, string strIdentity);
        [DllImport("LoggerDLL.dll", EntryPoint="UNREGISTERLOGGERCLIENT", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int UnregisterLoggerClient(ref int hIdentity);

        public static int ErrorCount
        {
            get
            {
                InitLoggerDll();
                int errorCount = 0;
                int warningCount = 0;
                long ftLastError = 0L;
                long ftLastWarning = 0L;
                if (GetLoggerStats("", ref errorCount, ref ftLastError, ref warningCount, ref ftLastWarning) <= 0)
                {
                    return -1;
                }
                return errorCount;
            }
        }

        public static int WarningCount
        {
            get
            {
                InitLoggerDll();
                int errorCount = 0;
                int warningCount = 0;
                long ftLastError = 0L;
                long ftLastWarning = 0L;
                if (GetLoggerStats("", ref errorCount, ref ftLastError, ref warningCount, ref ftLastWarning) <= 0)
                {
                    return -1;
                }
                return warningCount;
            }
        }
    }
}

