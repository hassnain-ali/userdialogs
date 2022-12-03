using Acr.UserDialogs;
using Sample.ViewModels;

namespace Samples;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        // the idea here is that you would dependency inject userdialogs
        BindingContext = new SettingsViewModel(UserDialogs.Instance);
    }
}
