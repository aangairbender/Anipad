using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad
{
    public static class Utils
    {
        /*public static void RunAppCopyForFile(string filename)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = exePath;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = filename;
            startInfo.UseShellExecute = false;

            Process.Start(startInfo);
        }*/

        public static string GetFilenameFromCommandLineArgs(string[] args)
        {
            if (args.Length == 1)
                return null;

            return args[0];
        }
    }
}
