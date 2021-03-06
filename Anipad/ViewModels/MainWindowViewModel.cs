﻿using System;
using System.Windows;
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
            ExitCommand = new RelayCommand<Window>(window =>
            {
                bool letExit = _textEditor.CallExit();
                if (letExit)
                    window.Close();
            });
            TextChangedCommand = new RelayCommand(() =>
            {
                _textEditor.AnyChangeMade = true;
            });
            ChangeBackgroundImageCommand = new RelayCommand(ChooseAndChangeBackgroundImage);
            ResetBackgroundImageToDefault = new RelayCommand(() =>
            {
                Config.BackgroundImageManager.Reset();
                Config.Save();
            });
        }

        private void ChooseAndChangeBackgroundImage()
        {
            string filename = _dialogService.ShowChooseBackgroundImageDialog();

            if (filename != null)
            {
                Config.BackgroundImageManager.Set(new BackgroundImage("", filename));
                Config.Save();
            }
        }

        public RelayCommand NewCommand { get; private set; }
        public RelayCommand OpenCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAsCommand { get; private set; }
        public RelayCommand<Window> ExitCommand { get; private set; }
        public RelayCommand TextChangedCommand { get; private set; }
        public RelayCommand ChangeBackgroundImageCommand { get; private set; }
        public RelayCommand ResetBackgroundImageToDefault { get; private set; }

        public TextEditor TextEditor => _textEditor;
        public TextEditorConfig Config => _config;
    }
}
