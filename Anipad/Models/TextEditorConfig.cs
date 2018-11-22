using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Anipad.Models
{
    public class TextEditorConfig : INotifyPropertyChanged
    {
        public BackgroundImageManager BackgroundImageManager { get; }

        private TextWrapping _textWrapping;
        public TextWrapping TextWrapping
        {
            get => _textWrapping;
            set
            {
                _textWrapping = value;
                OnPropertyChanged();
            }
        }

        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                OnPropertyChanged();
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        private TextEditorConfig()
        {
            BackgroundImageManager = new BackgroundImageManager();
            TextWrapping = Constants.DefaultTextWrapping;
            FontFamily = new FontFamily(Constants.DefautFontFamilyName);
            FontSize = Constants.DefaultFontSize;
        }

        public static TextEditorConfig Load()
        {
            if (Properties.Settings.Default.TextEditorConfig == "")
                return CreateNew();

            return JsonConvert.DeserializeObject<TextEditorConfig>(Properties.Settings.Default.TextEditorConfig);
        }

        private static TextEditorConfig CreateNew()
        {
            var config = new TextEditorConfig();
            config.Save();
            return config;
        }

        public void Save()
        {
            Properties.Settings.Default.TextEditorConfig = JsonConvert.SerializeObject(this);
            Properties.Settings.Default.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            Save();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
