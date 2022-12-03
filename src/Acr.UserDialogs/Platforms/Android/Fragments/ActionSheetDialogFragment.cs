using Acr.UserDialogs.Builders;
using Android.App;
using Android.Content;
using Android.Views;


namespace Acr.UserDialogs.Fragments;

public class ActionSheetAppCompatDialogFragment : AbstractAppCompatDialogFragment<ActionSheetConfig>
{
    protected override void SetDialogDefaults(Dialog dialog)
    {
        dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
        dialog.KeyPress += OnKeyPress;
        dialog.CancelEvent += (sender, args) => Config?.Cancel?.Action?.Invoke();

        var cancellable = Config.Cancel != null;
        dialog.SetCancelable(cancellable);
        dialog.SetCanceledOnTouchOutside(cancellable);
    }


    public override void OnCancel(IDialogInterface dialog)
    {
        base.OnCancel(dialog);
        Config?.Cancel?.Action?.Invoke();
    }


    public override void Dismiss()
    {
        base.Dismiss();
        Config?.Cancel?.Action?.Invoke();
    }


    protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
        base.OnKeyPress(sender, args);
        if (args.KeyCode != Keycode.Back)
            return;

        args.Handled = true;
        Dismiss();
    }


    protected override Dialog CreateDialog(ActionSheetConfig config) => new ActionSheetBuilder().Build(AppCompatActivity, config);
}
