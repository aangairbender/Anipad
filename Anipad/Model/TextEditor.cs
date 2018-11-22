using Anipad.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Anipad.Model
{
    public class TextEditor : INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _currentFilename = Constants.DefaultFilename;
        public string CurrentFilename
        {
            get => _currentFilename;
            private set
            {
                _currentFilename = value;
                OnPropertyChanged();
            }
        }

        private bool _anyChangeMade = false;
        public bool AnyChangeMade
        {
            get => _anyChangeMade;
            set
            {
                _anyChangeMade = value;
                OnPropertyChanged();
            }
        }

        private TextEditorConfig _config = TextEditorConfig.Load();
        public TextEditorConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                OnPropertyChanged();
            }
        }

        public void Open()
        {
            if (!SafeCloseCurrent())
                return;

            bool dialogResult = DialogService.ShowOpenFileDialog(out string filename);
            if (dialogResult == false)
                return;

            OpenFile(filename);
        }

        public void Save()
        {
            SaveCurrentFile();
        }

        public void SaveAs()
        {
            bool dialogResult = DialogService.ShowSaveFileDialog(out string filename);
            if (dialogResult == false)
                return;

            SaveFile(filename);
        }

        public void New()
        {
            if (!SafeCloseCurrent())
                return;
            NewFile();
        }

        public void Exit()
        {
            if (!SafeCloseCurrent())
                return;
            Application.Current.Shutdown();
        }

        private bool SafeCloseCurrent()
        {
            if (!AnyChangeMade)
                return true;

            bool? dialogResult = DialogService.ShowSaveChangesMessageBox(CurrentFilename);

            if (!dialogResult.HasValue)
                return false;

            if (dialogResult.Value == false)
                return true;

            return SaveCurrentFile();
        }

        private void OpenFile(string filename)
        {
            Text = File.ReadAllText(filename);
            CurrentFilename = filename;
            AnyChangeMade = false;
        }

        private void SaveFile(string filename)
        {
            File.WriteAllText(filename, Text);
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

            bool dialogResult = DialogService.ShowSaveFileDialog(out string filename);

            if (!dialogResult)
                return false;
            
            SaveFile(filename);
            return true;
        }

        public TextEditor()
        {
            ProcessCommandLineArgs();
        }
        
        private void ProcessCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 1)
                return;

            OpenFile(args[1]);

            for (int i = 2; i < args.Length; ++i)
                AppService.RunAppCopyForFile(args[i]);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
