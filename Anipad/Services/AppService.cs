using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad.Services
{
    public class AppService
    {
        public static void RunAppCopyForFile(string filename)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = exePath;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = filename;
            startInfo.UseShellExecute = false;

            Process.Start(startInfo);
        }
    }
}
