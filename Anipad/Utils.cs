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
        public static string[] ExtractFilesToOpenFromCommandLineArgs(string[] args)
        {
            if (args.Length == 1)
                return new string[] {null};

            return args.Skip(1).ToArray();
        }
    }
}
