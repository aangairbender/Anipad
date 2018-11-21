using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Anipad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DefaultFilename = "Unnamed";
        private const string TitlePattern = "{0}{1} - Anipad";

        private string _backgroundImageFilename = "";
        private string _currentFilename = "";
        private bool _anyChangeMade = false;

        private string CurrentFilename
        {
            get => _currentFilename;
            set
            {
                _currentFilename = value;
                UpdateWindowTitle();
            }
        }
        
        private string BackgroundImageFilename
        {
            get => _backgroundImageFilename;
            set
            {
                _backgroundImageFilename = value;
                ChangeBackground(_backgroundImageFilename);
                Properties.Settings.Default.BackgroundImageFilename = _backgroundImageFilename;
                Properties.Settings.Default.Save();
            }
        }

        private bool AnyChangeMade
        {
            get => _anyChangeMade;
            set
            {
                _anyChangeMade = value;
                UpdateWindowTitle();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadSettings();
            CurrentFilename = DefaultFilename;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                OpenFile(args[1]);
            }
        }

        private void UpdateWindowTitle()
        {
            Title = string.Format(TitlePattern, (AnyChangeMade ? "*" : ""), _currentFilename);
        }

        private void ChangeBackgroundImage_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.DefaultExt = "Picture";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value == true)
            {
                string filename = dlg.FileName;
                BackgroundImageFilename = filename;
            }
        }

        private void ResetBackgroundImage_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            BackgroundImageFilename = (string)Properties.Settings.Default.Properties["BackgroundImageFilename"].DefaultValue;
        }

        private void ChangeBackground(string filename)
        {
            try
            {
                var bitmapImage = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
                backgroundImage.Source = bitmapImage;
            } catch (Exception e)
            {
                
            }
        }

        private void LoadSettings()
        {
            BackgroundImageFilename = Properties.Settings.Default.BackgroundImageFilename;
        }


        private void OpenFile(string filename)
        {
            try
            {
                textEditor.Clear();
                using (StreamReader sr = new StreamReader(filename))
                {
                    textEditor.AppendText(sr.ReadToEnd());
                }
                CurrentFilename = filename;
                AnyChangeMade = false;
            } catch (Exception e)
            {

            }
        }

        private void textEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            AnyChangeMade = true;
        }

        private void New_MenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            textEditor.Clear();
            CurrentFilename = DefaultFilename;
            AnyChangeMade = false;
        }

        private void Open_MenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Any file (*.*) | *.*";

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value == true)
            {
                string filename = dlg.FileName;
                OpenFile(filename);
            }
        }

        private void Save_MenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentFilename == DefaultFilename)
            {
                var dlg = new SaveFileDialog();
                dlg.Filter = "Any file (*.*) | *.*";

                bool? result = dlg.ShowDialog();

                if (result.HasValue && result.Value == true)
                {
                    string filename = dlg.FileName;
                    CurrentFilename = filename;
                }
            }
            SaveToFile(CurrentFilename);
        }

        private void SaveToFile(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(textEditor.Text);
                    }
                }
                AnyChangeMade = false;
            }
            catch (Exception e)
            {

            }
        }

        public static RoutedCommand OpenCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand NewCommand = new RoutedCommand();

    }
}
