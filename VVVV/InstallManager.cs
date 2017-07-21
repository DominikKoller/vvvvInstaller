using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;
using System.Drawing;

namespace VVVV
{
    public partial class InstallerForm : Form
    {
        private string appPath = Application.StartupPath;
        private string installPath;

        private Color inActiveColor = Color.Gray;
        private Color activeColor = Color.Red;

        public Action V4InstallDone;
        public bool CheckDeps()
        {
            return CheckDirectX9c() && CheckDotNet46() && 
                CheckVCPP2008() && CheckVCPP2010() && 
                CheckVCPP2012() && CheckVCPP2013() && 
                CheckVCPP2017();
        }

        
        public void LoadV4()
        {
            var url64 = "https://vvvv.org/sites/default/files/vvvv_50beta35.8_x64.zip";
            var url32 = "https://vvvv.org/sites/default/files/vvvv_50beta35.8_x86.zip";
            progressBar1.Visible = true;

            var wc = new WebClient();
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            DownloadHelper.LoadAsset(wc, url32, url64, "core", force32CheckBox.Checked);
        }


        public void LoadV4Addons()
        {
            var url64 = "https://vvvv.org/sites/default/files/addons_50beta35.8_x64.zip";
            var url32 = "https://vvvv.org/sites/default/files/addons_50beta35.8_x86.zip";
            
            var wc = new WebClient();
            wc.DownloadFileCompleted += wc_DownloadAddonFileCompleted;
            DownloadHelper.LoadAsset(wc, url32, url64, "addon", force32CheckBox.Checked);

        }


        private void UnpackV4()
        {
            if (addAddonsCheck.Checked)
            {
                while (!AddonsDownloadDone) // not so nice but who cares :)
                { }
            }
            ZipFile.ExtractToDirectory(Application.UserAppDataPath + "/tmp_core", installPath);
            ZipFile.ExtractToDirectory(Application.UserAppDataPath + "/tmp_addon", installPath + "/"+ V4version);
            Console.WriteLine("fin--> create shortcut");
            Shortcut.Create("VVVV", installPath + @"/"+ V4version +@"/vvvv.exe", @"C:\ProgramData\Microsoft\Windows\Start Menu\");
            Runner.StartExeWithArguments(Environment.SystemDirectory + @"/regsvr32.exe", "/s " + installPath + @"/"+V4version+@"/lib/thirdparty/x64/AddFlow5.ocx");
            Runner.StartExeWithArguments(Environment.SystemDirectory + @"/regsvr32.exe", "/s " + installPath + @"/" + V4version + @"/lib/thirdparty/x86/AddFlow5.ocx");
            V4InstallDone();
        }


        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }
        

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            UnpackV4();
            progressBar1.Visible = false;
            ((WebClient)sender).Dispose();
        }


        private bool AddonsDownloadDone = false;
        void wc_DownloadAddonFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            AddonsDownloadDone = true;
            ((WebClient)sender).Dispose();
        }
        
    }
}
