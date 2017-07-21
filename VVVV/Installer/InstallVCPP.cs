using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VVVV
{
    public class InstallVCPP
    {
        public Action installdone;
        private DownloadProgressChangedEventHandler pb;
        public InstallVCPP(Action callAfterInstall, DownloadProgressChangedEventHandler progressbar)
        {
            installdone += callAfterInstall;
            pb = progressbar;
        }

        private string vcppName;
        public  bool Install(string url32, string url64, string name, bool force32)
        {
            vcppName = name; 
            var wc = new WebClient();
            wc.DownloadProgressChanged += pb;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            DownloadHelper.LoadAsset(wc, url32, url64, vcppName+".exe", force32);

            return false;
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            installvcpp();
            ((WebClient)sender).Dispose();
        }

        private void installvcpp()
        {
            Runner.StartExeWithArguments(Application.UserAppDataPath + "/tmp_"+vcppName+".exe", " /q");
            installdone();
        }
    }
}
