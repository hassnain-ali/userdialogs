using System;
using Acr.UserDialogs.Builders;
using Android.App;
using Android.Content;
using Android.Views;


namespace Acr.UserDialogs.Fragments;

public class TimeAppCompatDialogFragment : AbstractAppCompatDialogFragment<TimePromptConfig>
{
    protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
        base.OnKeyPress(sender, args);
        if (args.KeyCode != Keycode.Back)
            return;

        args.Handled = true;
        if (Config.IsCancellable)
        {
            Config?.OnAction?.Invoke(new TimePromptResult(false, TimeSpan.MinValue));
            Dismiss();
        }
    }


    protected override void SetDialogDefaults(Dialog dialog)
    {
        dialog.SetCancelable(false);
        dialog.SetCanceledOnTouchOutside(false);
        dialog.KeyPress += OnKeyPress;
    }

    [Obsolete]
    protected override Dialog CreateDialog(TimePromptConfig config)
    {
        return TimePromptBuilder.Build(AppCompatActivity, config);
    }
}
