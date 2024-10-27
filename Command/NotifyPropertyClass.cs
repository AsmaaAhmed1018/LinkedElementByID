using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinkedElementByID
{
    public abstract class NotifyPropertyClass : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        public void OnPropertyChanged([CallerMemberName] string PropName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(PropName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
