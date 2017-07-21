using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VVVV
{
    public static class DownloadHelper
    {
        public static void LoadAsset(WebClient wc, string url32, string url64, string type, bool force32)
        {
            var is64 = is64BitOperatingSystem;
            if (!is64 || force32)
            {
                wc.DownloadFileAsync(new System.Uri(url32),
                Application.UserAppDataPath + "/tmp_" + type);
            }
            else
            {
                wc.DownloadFileAsync(new System.Uri(url64),
               Application.UserAppDataPath + "/tmp_" + type);
            }
        }
        

        static bool is64BitProcess = (IntPtr.Size == 8);
        public static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
        
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        private static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
