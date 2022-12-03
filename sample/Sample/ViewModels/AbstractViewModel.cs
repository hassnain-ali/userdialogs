using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;

namespace Sample.ViewModels;

public abstract class AbstractViewModel : INotifyPropertyChanged
{
    protected AbstractViewModel(IUserDialogs dialogs)
    {
        Dialogs = dialogs;
    }


    protected IUserDialogs Dialogs { get; }


    protected virtual void Result(string msg)
    {
        _ = Dialogs.Alert(msg);
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
