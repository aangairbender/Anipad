using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad.Services
{
    public interface IFileService
    {
        string ReadFile(string filename);

        void WriteFile(string filename, string contents);
    }
}
