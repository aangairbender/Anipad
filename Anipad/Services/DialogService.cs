using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Anipad.Services
{
    public static class DialogService
    {
        public static bool ShowOpenFileDialog(out string filename) =>
            ShowOpenFileDialog(Constants.AnyFileFilter, false, out filename);

        public static bool ShowSaveFileDialog(out string filename) =>
            ShowSaveFileDialog(Constants.AnyFileFilter, out filename);

        public static bool? ShowSaveChangesMessageBox(string filename)
        {
            string caption = Constants.AppName;
            string messageBoxText = string.Format(Constants.SaveChangesMessageBoxTextPattern, filename);
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNoCancel);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return null;
            }
        }

        private static bool ShowOpenFileDialog(string filter, bool multiselect, out string filename)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = multiselect;

            bool? result = openFileDialog.ShowDialog();
            filename = openFileDialog.FileName;

            return result.HasValue ? result.Value : false;
        }

        private static bool ShowSaveFileDialog(string filter, out string filename)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;

            bool? result = saveFileDialog.ShowDialog();
            filename = saveFileDialog.FileName;

            return result.HasValue ? result.Value : false;
        }
    }
}
