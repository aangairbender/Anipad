using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Anipad.Services;

namespace Anipad.Models
{
    public class TextEditor : INotifyPropertyChanged
    {
        private readonly Func<string> _chooseFileToOpen;
        private readonly Func<string> _chooseFileToSave;
        private readonly Func<string, bool?> _confirmSaveChanges;
        private readonly Func<string, string> _readFile;
        private readonly Action<string, string> _writeFile;

        private string _text;
        private string _currentFilename = Constants.DefaultFilename;
        private bool _anyChangeMade;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string CurrentFilename
        {
            get => _currentFilename;
            private set
            {
                _currentFilename = value;
                OnPropertyChanged();
            }
        }

        public bool AnyChangeMade
        {
            get => _anyChangeMade;
            set
            {
                _anyChangeMade = value;
                OnPropertyChanged();
            }
        }

        public TextEditor(Func<string> chooseFileToOpen, Func<string> chooseFileToSave,
            Func<string, bool?> confirmSaveChanges, Func<string, string> readFile, Action<string, string> writeFile, string initialFilename = null)
        {
            _chooseFileToOpen = chooseFileToOpen;
            _chooseFileToSave = chooseFileToSave;
            _confirmSaveChanges = confirmSaveChanges;
            _readFile = readFile;
            _writeFile = writeFile;

            if (initialFilename != null)
            {
                OpenFile(initialFilename);
            }
        }

        public void CallOpen()
        {
            if (!SafeCloseCurrent())
                return;

            string fileToOpen = _chooseFileToOpen();

            if (fileToOpen == null)
                return;

            OpenFile(fileToOpen);
        }

        public void CallSave()
        {
            SaveCurrentFile();
        }

        public void CallSaveAs()
        {
            AskForFileAndSave();
        }

        public void CallNew()
        {
            if (!SafeCloseCurrent())
                return;

            NewFile();
        }

        public bool CallExit()
        {
            return SafeCloseCurrent();
        }

        private bool SafeCloseCurrent()
        {
            if (!AnyChangeMade)
                return true;

            bool? dialogResult = _confirmSaveChanges(CurrentFilename);

            if (!dialogResult.HasValue)
                return false;

            if (dialogResult == false)
                return true;

            return SaveCurrentFile();
        }

        private void OpenFile(string filename)
        {
            Text = _readFile(filename);
            CurrentFilename = filename;
            AnyChangeMade = false;
        }

        private void SaveFile(string filename)
        {
            _writeFile(filename, Text);
            CurrentFilename = filename;
            AnyChangeMade = false;
        }

        private void NewFile()
        {
            Text = "";
            CurrentFilename = Constants.DefaultFilename;
            AnyChangeMade = false;
        }

        private bool SaveCurrentFile()
        {
            if (!AnyChangeMade)
                return true;

            if (CurrentFilename != Constants.DefaultFilename)
            {
                SaveFile(CurrentFilename);
                return true;
            }

            return AskForFileAndSave();
        }

        private bool AskForFileAndSave()
        {
            string fileToSave = _chooseFileToSave();

            if (fileToSave == null)
                return false;

            SaveFile(fileToSave);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
