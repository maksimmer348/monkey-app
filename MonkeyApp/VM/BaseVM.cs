using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyApp.VM;

//[INotifyPropertyChanged]
public partial class BaseVM : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    [ObservableProperty]
    string title;
    public bool IsNotBusy => !IsBusy;
}
