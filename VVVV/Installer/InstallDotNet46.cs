using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VVVV
{
    public static class DotNet46Installer
    {
        public static Action installDone;

        public static bool InstallDotNet46(DownloadProgressChangedEventHandler progressbarUpdate)
        {
            var url = "https://download.microsoft.com/download/C/3/A/C3A5200B-D33C-47E9-9D70-2F7C65DAAD94/NDP46-KB3045557-x86-x64-AllOS-ENU.exe";
            var wc = new WebClient();
            wc.DownloadProgressChanged += progressbarUpdate;
            wc.DownloadFileCompleted += wc_Downloaddotnet46FileCompleted;
            DownloadHelper.LoadAsset(wc, url, url, "dotnet46.exe", false);

            return false;
        }

        private static void wc_Downloaddotnet46FileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("dotnet download done");  // if no error : error ==null
            installdotnet();
            ((WebClient)sender).Dispose();
        }

        private static void installdotnet()
        {
            Runner.StartExeWithArguments(Application.UserAppDataPath + "/tmp_dotnet46.exe"," /q /log "+ Application.UserAppDataPath+"dotnetinstall.log");
            installDone();
        }
    }
}
