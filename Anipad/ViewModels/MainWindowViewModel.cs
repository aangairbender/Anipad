using Anipad.Models;
using Anipad.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace Anipad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TextEditor _textEditor;
        private readonly TextEditorConfig _config;

        private readonly IDialogService _dialogService = SimpleIoc.Default.GetInstance<IDialogService>();
        private readonly IFileService _fileService = SimpleIoc.Default.GetInstance<IFileService>();

        public MainWindowViewModel(string initialFilename)
        {
            _textEditor = new TextEditor(_dialogService.ShowOpenFileDialog, _dialogService.ShowSaveFileDialog,
                _dialogService.ShowSaveChangesMessageBox, _fileService.ReadFile, _fileService.WriteFile, initialFilename);

            _config = TextEditorConfig.Load();

            InitializeCommands();
        }
        

        private void InitializeCommands()
        {
            NewCommand = new RelayCommand(_textEditor.CallNew);
            OpenCommand = new RelayCommand(_textEditor.CallOpen);
            SaveCommand = new RelayCommand(_textEditor.CallSave);
            SaveAsCommand = new RelayCommand(_textEditor.CallSaveAs);
            ExitCommand = new RelayCommand(_textEditor.CallExit);
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
        public TextEditorConfig Config => _config;
    }
}
