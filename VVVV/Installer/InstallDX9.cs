using System;
using System.Collections;
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
    public static class DX9Installer
    {
        public static Action installDone;

        public static bool InstallDirectX9c(DownloadProgressChangedEventHandler progressbarUpdate)
        {
            // installer: https://download.microsoft.com/download/8/4/A/84A35BF1-DAFE-4AE8-82AF-AD2AE20B6B14/directx_Jun2010_redist.exe
            // webinstaller: https://download.microsoft.com/download/1/7/1/1718CCC4-6315-4D8E-9543-8E28A4E18C4C/dxwebsetup.exe
            // download installer
            // run directx_June201_redist.ext /Q /T:"tmp_path"
            // run DXSETUP.exe /silent
            // done
            var url = "https://download.microsoft.com/download/8/4/A/84A35BF1-DAFE-4AE8-82AF-AD2AE20B6B14/directx_Jun2010_redist.exe";
            var wc = new WebClient();
            wc.DownloadProgressChanged += progressbarUpdate;
            wc.DownloadFileCompleted += wc_DownloadDx9FileCompleted;
            DownloadHelper.LoadAsset(wc, url, url, "dx9.exe", false);

            return true;
        }

        private static void wc_DownloadDx9FileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("dx9 download done");  // if no error : error ==null
            installDX9();
            ((WebClient)sender).Dispose();
        }
        
        private static void installDX9()
        {
            var dx_name = Application.UserAppDataPath + "/tmp_dx9.exe";
            Runner.StartExeWithArguments(dx_name, " /Q  /T:" + Application.UserAppDataPath + "//dx");

            dx_name = Application.UserAppDataPath + "//dx//DXSETUP.exe";
            Runner.StartExeWithArguments(dx_name, " /silent");
            
            if (System.IO.Directory.Exists(Application.UserAppDataPath + "//dx")) 
                System.IO.Directory.Delete(Application.UserAppDataPath + "//dx",true);
            installDone();
        }
    }
}
