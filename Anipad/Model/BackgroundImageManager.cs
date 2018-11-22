using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anipad.Model
{
    public class BackgroundImageManager : INotifyPropertyChanged
    {
        private BackgroundImage _current;
        public BackgroundImage Current
        {
            get => _current;
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BackgroundImage> BackgroundImages { get; } = new ObservableCollection<BackgroundImage>();


        public void Add(BackgroundImage backgroundImage)
        {
            BackgroundImages.Add(backgroundImage);
        }

        public void Remove(BackgroundImage backgroundImage)
        {
            BackgroundImages.Remove(backgroundImage);
        }

        public static BackgroundImageManager CreateDefault()
        {
            var backgroundImageManager = new BackgroundImageManager();
            backgroundImageManager.Add(new BackgroundImage(
                Constants.DefaultBackgroundImageTitle,
                Constants.DefaultBackgroundImageFilename)
                );
            backgroundImageManager.Current = backgroundImageManager.BackgroundImages.First();

            return backgroundImageManager;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
