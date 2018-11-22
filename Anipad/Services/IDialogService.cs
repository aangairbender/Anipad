using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anipad.Services
{
    public interface IDialogService
    {
        string ShowOpenFileDialog();

        string ShowSaveFileDialog();

        bool? ShowSaveChangesMessageBox(string filename);
    }
}
