using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Anipad.Models;
using Anipad.Services;
using Anipad.ViewModels;
using Anipad.Views;
using GalaSoft.MvvmLight.Ioc;

namespace Anipad
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            SetupIoc();
            string initialFilename = Utils.GetFilenameFromCommandLineArgs(Environment.GetCommandLineArgs());


            var mainWindowViewModel = new MainWindowViewModel(initialFilename);

            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }


        private void SetupIoc()
        {
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
            SimpleIoc.Default.Register<IFileService>(() => new FileService());
        }
        
    }
}
