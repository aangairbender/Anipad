using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Anipad.Model;
using System.Windows.Data;
using Anipad.Converters;

namespace Anipad.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TextEditor _textEditor = new TextEditor();

        public MainWindowViewModel()
        {
            InitializeCommands();
        }
        

        private void InitializeCommands()
        {
            NewCommand = new RelayCommand(_textEditor.New);
            OpenCommand = new RelayCommand(_textEditor.Open);
            SaveCommand = new RelayCommand(_textEditor.Save);
            SaveAsCommand = new RelayCommand(_textEditor.SaveAs);
            ExitCommand = new RelayCommand(_textEditor.Exit);
            TextChangedCommand = new RelayCommand(() =>
            {
                _textEditor.AnyChangeMade = true;
            });
        }

        public RelayCommand NewCommand { get; private set; }
        public RelayCommand OpenCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAsCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        public RelayCommand TextChangedCommand { get; private set; }

        public TextEditor TextEditor => _textEditor;
    }
}
