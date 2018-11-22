using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Anipad.Models
{
    public class BackgroundImageManager : INotifyPropertyChanged
    {
        private BackgroundImage _current = BackgroundImage.Default;

        public BackgroundImage Current
        {
            get => _current;
            private set
            {
                if (_current == value)
                    return;
                
                _current = value;
                OnPropertyChanged();
            }
        }

        public void Set(BackgroundImage backgroundImage)
        {
            Current = backgroundImage;
        }

        public void Reset()
        {
            Current = BackgroundImage.Default;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
