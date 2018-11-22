using System.Windows;
using Microsoft.Win32;

namespace Anipad.Services
{
    public class DialogService : IDialogService
    {
        public string ShowOpenFileDialog() =>
            ShowOpenFileDialog(Constants.AnyFileFilter);

        public string ShowSaveFileDialog() =>
            ShowSaveFileDialog(Constants.AnyFileFilter);

        public bool? ShowSaveChangesMessageBox(string filename)
        {
            string messageBoxCaption = Constants.AppName;
            string messageBoxText = string.Format(Constants.SaveChangesMessageBoxTextPattern, filename);
            MessageBoxResult result = MessageBox.Show(messageBoxText, messageBoxCaption, MessageBoxButton.YesNoCancel);

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

        private string ShowOpenFileDialog(string filter)
        {
            var openFileDialog = new OpenFileDialog {Filter = filter, Multiselect = false};

            bool? result = openFileDialog.ShowDialog();
            string filename = openFileDialog.FileName;

            return result == true ? filename : null;
        }

        private string ShowSaveFileDialog(string filter)
        {
            var saveFileDialog = new SaveFileDialog {Filter = filter};

            bool? result = saveFileDialog.ShowDialog();
            string filename = saveFileDialog.FileName;

            return result == true ? filename : null;
        }
    }
}
