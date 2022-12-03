using Acr.UserDialogs.Builders;
using Android.App;
using Android.Content;
using Android.Views;

namespace Acr.UserDialogs.Fragments;

public class ConfirmAppCompatDialogFragment : AbstractAppCompatDialogFragment<ConfirmConfig>
{
    protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
        base.OnKeyPress(sender, args);
        if (args.KeyCode != Keycode.Back)
            return;

        args.Handled = true;
        Config?.OnAction?.Invoke(false);
        Dismiss();
    }


    protected override Dialog CreateDialog(ConfirmConfig config)
    {
        return new ConfirmBuilder().Build(AppCompatActivity, config);
    }
}
