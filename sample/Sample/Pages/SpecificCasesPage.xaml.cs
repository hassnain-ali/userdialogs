using Acr.UserDialogs;


using Sample.ViewModels;
namespace Samples;

public partial class SpecificCasesPage : ContentPage
{
    public SpecificCasesPage()
    {
        InitializeComponent();

        // the idea here is that you would dependency inject userdialogs
        BindingContext = new SpecificCasesViewModel(UserDialogs.Instance);
    }
}
