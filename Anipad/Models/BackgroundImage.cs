using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Anipad.Models
{
    public class BackgroundImage : INotifyPropertyChanged
    {
        public static BackgroundImage Default => new BackgroundImage(Constants.DefaultBackgroundImageTitle,
            Constants.DefaultBackgroundImageFilename);

        private string _title;
        private string _filename;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                OnPropertyChanged();
            }
        }

        public BackgroundImage(string title, string filename)
        {
            Title = title;
            Filename = filename;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
