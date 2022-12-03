using Acr.UserDialogs;
using Sample.ViewModels;

namespace Samples;

public partial class ToastsPage : ContentPage
{
    public ToastsPage()
    {
        InitializeComponent();

        // the idea here is that you would dependency inject userdialogs
        BindingContext = new ToastsViewModel(UserDialogs.Instance);
    }
}
