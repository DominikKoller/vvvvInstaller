using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VVVV
{
    public partial class InstallerForm
    {
        public string OpenDialog()
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return @"C:\vvvv";
        }

        public bool CheckDirectX9c()
        {
            // Assembly assembly = Assembly.LoadFrom("MyNice.dll");

            var dir = Environment.SystemDirectory;
            bool allThere = true;

            for (int i = 24; i < 44; i++)
            {
                var dllName = dir + @"\d3dx9_" + i + ".dll";
                var exists = File.Exists(@dllName);
                if(!exists)
                {
                    Console.WriteLine("whut?");
                }
                allThere = (allThere && exists);
            }

            string partialName = "d3dx9";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"c:\windows\System32");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(partialName + "*.dll");
         
            if (filesInDir.Count() < 20)
                allThere = false;

            return allThere;
        }

        // check registry keys
        // here are almost all of them listed:
        // https://stackoverflow.com/questions/12206314/detect-if-visual-c-redistributable-for-visual-studio-2012-is-installed
        public bool CheckDotNet46()
        {
            return GetDotNetVersion.Get45PlusFromRegistry();
        }

        public bool CheckVCPP2008()
        {
            return checkRegKey(@"SOFTWARE\Classes\Installer\Products\EFEE0228DC83E77358593193D847A0EC"); // ok
        }

        public bool CheckVCPP2010()
        {
            return checkRegKey(@"SOFTWARE\Classes\Installer\Products\1926E8D15D0BCE53481466615F760A7F"); // ok
        }

        public bool CheckVCPP2012()
        {
            return checkRegKey(@"SOFTWARE\Classes\Installer\Dependencies\{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}"); // ok
        }

        public bool CheckVCPP2013()
        {
            return checkRegKey(@"SOFTWARE\Classes\Installer\Dependencies\{050d4fc8-5d48-4b8f-8972-47c82c46020f}"); // ok
        }

        public bool CheckVCPP2017()
        {
            return checkRegKey(@"SOFTWARE\Classes\Installer\Dependencies\,,amd64,14.0,bundle");
        }

        public bool CheckAddFlow()
        {
            return false;
        }

        private bool checkRegKey(string key)
        {
            using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(key))
                if (Key != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}
