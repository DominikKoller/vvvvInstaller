using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVVV
{
    static class Runner
    {
        public static void StartExeWithArguments(string exeName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = exeName;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments;
            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("failed with: " + e);
            }
        }
    }
}
