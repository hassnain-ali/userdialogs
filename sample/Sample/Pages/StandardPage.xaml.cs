using Acr.UserDialogs;
using Sample.ViewModels;

namespace Samples;

public partial class StandardPage : ContentPage
{
    public StandardPage()
    {
        InitializeComponent();

        // the idea here is that you would dependency inject userdialogs
        BindingContext = new StandardViewModel(UserDialogs.Instance);
    }
}
