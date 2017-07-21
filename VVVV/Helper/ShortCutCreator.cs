using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWshRuntimeLibrary;

namespace VVVV
{
  
    public static class Shortcut
    {
        public static void Create(string text, string sourceFile, string destPath)
        {
            if (destPath.Substring(destPath.Length - 1, 1) != "\\")
                destPath += "\\";

            string shortcutPath = destPath + text + ".lnk";
            WshShell shell = new WshShell();
            IWshShortcut link = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            link.TargetPath = sourceFile;
            link.Save();
        }
    }
}
