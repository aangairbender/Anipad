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
using Newtonsoft.Json;

namespace Anipad.Model
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
            BackgroundImageManager = BackgroundImageManager.CreateDefault();
            TextWrapping = Constants.DefaultTextWrapping;
            FontFamily = new FontFamily(Constants.DefautFontFamilyName);
            FontSize = Constants.DefaultFontSize;
        }

        public static TextEditorConfig Load()
        {
            if (!File.Exists(Constants.TextEditorConfigFilename))
                return Create();

            return JsonConvert.DeserializeObject<TextEditorConfig>(
                File.ReadAllText(Constants.TextEditorConfigFilename));
        }

        private static TextEditorConfig Create()
        {
            var config = new TextEditorConfig();
            config.Save();
            return config;
        }

        public void Save()
        {
            File.WriteAllText(Constants.TextEditorConfigFilename, JsonConvert.SerializeObject(this));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
