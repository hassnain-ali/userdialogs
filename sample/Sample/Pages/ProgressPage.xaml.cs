using Acr.UserDialogs;
using Sample.ViewModels;

namespace Samples;

public partial class ProgressPage : ContentPage
{
    public ProgressPage()
    {
        InitializeComponent();

        // the idea here is that you would dependency inject userdialogs
        BindingContext = new ProgressViewModel(UserDialogs.Instance);
    }
}
