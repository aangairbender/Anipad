using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad
{
    public static class Helpers
    {
        public static string BuildTitle(string currentFilename, bool anyChangeMade)
        {
            return (anyChangeMade ? "*" : "")
                + (currentFilename == "" ? Constants.DefaultFilename : currentFilename)
                + " - "
                + Constants.AppName;
        }
    }
}
