using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace VVVV
{
    public partial class InstallerForm : Form
    {
        private int mouseX = 0, mouseY = 0;
        private int mouseOffsetX = 0, mouseOffsetY = 0;
        private bool mouseDown;
        private Timer timer1;

        private string V4version;

        // todo status text?

        public InstallerForm()
        {
            InitializeComponent();
            InitTimer();
            installPath = @"C:\vvvv";
        }

      
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 200; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var depsOk = CheckDeps();
            DepsIndicator.BackColor = depsOk ? Color.Green : Color.Red;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffsetX = e.Location.X;
            mouseOffsetY = e.Location.Y;
            mouseDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X;
                mouseY = MousePosition.Y;

                this.SetDesktopLocation(mouseX - mouseOffsetX, mouseY - mouseOffsetY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            installPath = InstallPathBox.Text;

            if (force32CheckBox.Checked && !DownloadHelper.is64BitOperatingSystem)
                V4version = @"vvvv_50beta35.8_x86";
            else
                V4version = @"vvvv_50beta35.8_x64";

            if (installPath == "")
                return; // todo check if path is invalid
                        // todo dialogbox to user

            if (Directory.Exists(installPath + V4version))
            {
                return; // todo dialogbox to user
            }

            choosePathButton.Enabled = false;
            addAddonsCheck.Enabled = false;
            StartButton.Enabled = false;
            force32CheckBox.Enabled = false;

            V4InstallDone += DirectX9Install;
            
            if (!Directory.Exists(installPath))
                Directory.CreateDirectory(installPath, new System.Security.AccessControl.DirectorySecurity());
            
            LoadV4();
            if (addAddonsCheck.Checked)
            {
                LoadV4Addons();
            }
        }

        private void choosePathButton_Click(object sender, EventArgs e)
        {
           installPath = OpenDialog();
           InstallPathBox.Text = installPath;
        }

        //-------------------------------------------------------------------------
        // Deps install event chain starts here
        private void GetAllDepsButton_Click(object sender, EventArgs e)
        {
            DirectX9Install();
        }

        private void DirectX9Install()
        {
            DX9Installer.installDone += DotNetInstall;    //queue next Dependency install
            if (!CheckDirectX9c())
                DX9Installer.InstallDirectX9c(wc_DownloadProgressChanged);
            else
                DX9Installer.installDone();
        }

        private void DotNetInstall()
        {
            DotNet46Installer.installDone += vcpp2008Install; //queue next Dependency install
            if (!CheckDotNet46())
                DotNet46Installer.InstallDotNet46(wc_DownloadProgressChanged);
            else
                DotNet46Installer.installDone();
        }
       
        private void vcpp2008Install()
        {
            InstallVCPP installer = new InstallVCPP(vcpp2010Install, wc_DownloadProgressChanged); //queue next Dependency install
            if (!CheckVCPP2008())
            {
                var url32 = "https://download.microsoft.com/download/d/d/9/dd9a82d0-52ef-40db-8dab-795376989c03/vcredist_x86.exe"; // ok
                var url64 = "https://download.microsoft.com/download/2/d/6/2d61c766-107b-409d-8fba-c39e61ca08e8/vcredist_x64.exe"; // ok

                installer.Install(url32, url64, "vcpp2008", force32CheckBox.Checked);
            }
            else
                installer.installdone();
        }
     
        private void vcpp2010Install()
        {
            InstallVCPP installer = new InstallVCPP(vcpp2012Install, wc_DownloadProgressChanged); //queue next Dependency install
            if (!CheckVCPP2010())
            {
                var url32 = "https://download.microsoft.com/download/C/6/D/C6D0FD4E-9E53-4897-9B91-836EBA2AACD3/vcredist_x86.exe"; // ok
                var url64 = "https://download.microsoft.com/download/A/8/0/A80747C3-41BD-45DF-B505-E9710D2744E0/vcredist_x64.exe"; // ok

                installer.Install(url32, url64, "vcpp2010", force32CheckBox.Checked);
            }
            else
                installer.installdone();
        }
       
        private void vcpp2012Install()
        {
            InstallVCPP installer = new InstallVCPP(vcpp2013Install, wc_DownloadProgressChanged); //queue next Dependency install
            if (!CheckVCPP2012())
            {
                var url32 = "https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x86.exe"; // ok
                var url64 = "https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x64.exe"; // ok

                installer.Install(url32, url64, "vcpp2012", force32CheckBox.Checked);
            }
            else
                installer.installdone();
        }
        
        private void vcpp2013Install()
        {
            InstallVCPP installer = new InstallVCPP(vcpp2017Install, wc_DownloadProgressChanged); //queue next Dependency install
            if (!CheckVCPP2013())
            {
                var url32 = "https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x86.exe"; // ok
                var url64 = "https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x64.exe"; // ok

                installer.Install(url32, url64, "vcpp2013", force32CheckBox.Checked);
            }
            else
                installer.installdone();
        }
        
        private void vcpp2017Install()
        {
            InstallVCPP installer = new InstallVCPP(AddFlowInstall, wc_DownloadProgressChanged); //queue next Dependency install
            if (!CheckVCPP2017())
            {
                var url32 = "https://download.microsoft.com/download/7/a/6/7a68af9f-3761-4781-809b-b6df0f56d24c/vc_redist.x86.exe"; // ok
                var url64 = "https://download.microsoft.com/download/8/9/d/89d195e1-1901-4036-9a75-fbe46443fc5a/vc_redist.x64.exe"; // ok

                installer.Install(url32, url64, "vcpp2017", force32CheckBox.Checked);
            }
            else
                installer.installdone();
        }
      
        private void AddFlowInstall()
        {
            //todo check for 32 bit version!
            Runner.StartExeWithArguments(Environment.SystemDirectory + @"/regsvr32.exe", "/s " + installPath + @"/"+V4version+@"/lib/thirdparty/x64/AddFlow5.ocx");
            Runner.StartExeWithArguments(Environment.SystemDirectory + @"/regsvr32.exe", "/s " + installPath + @"/"+V4version+@"/lib/thirdparty/x86/AddFlow5.ocx");
            progressBar1.Value = 0;
        }

        // Deps install event chain ends here
        //-------------------------------------------------------------------------


        // other button click calls
        private void DirectX9Button_Click(object sender, EventArgs e)
        {
            DirectX9Install();
        }

        private void NetButton_Click(object s, EventArgs e)
        {
            DotNetInstall();
        }

        private void vcpp2008Button_Click(object sender, EventArgs e)
        {
            vcpp2008Install();
        }

        private void vcpp2010Button_Click(object sender, EventArgs e)
        {
            vcpp2008Install();
        }

        private void vcpp2012Button_Click(object sender, EventArgs e)
        {
            vcpp2012Install();
        }

        private void vcpp2013Button_Click(object sender, EventArgs e)
        {
            vcpp2013Install();
        }

        private void vcpp2017Button_Click(object sender, EventArgs e)
        {
            vcpp2017Install();
        }
        

        private void AddFlowButton_Click(object sender, EventArgs e)
        {
           
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
