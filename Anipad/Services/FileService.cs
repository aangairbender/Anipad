using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad.Services
{
    public class FileService : IFileService
    {
        public string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        public void WriteFile(string filename, string contents)
        {
            File.WriteAllText(filename, contents);
        }
    }
}
