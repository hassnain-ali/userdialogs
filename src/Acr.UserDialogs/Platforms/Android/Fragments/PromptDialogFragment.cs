using Acr.UserDialogs.Builders;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;


namespace Acr.UserDialogs.Fragments;

public class PromptAppCompatDialogFragment : AbstractAppCompatDialogFragment<PromptConfig>
{
    protected override void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
        base.OnKeyPress(sender, args);
        args.Handled = false;

        switch (args.KeyCode)
        {
            case Keycode.Back:
                args.Handled = true;
                if (Config.IsCancellable)
                    SetAction(false);
                break;

            case Keycode.Enter:
                args.Handled = true;
                SetAction(true);
                break;
        }
    }

    protected override Dialog CreateDialog(PromptConfig config)
    {
        return new PromptBuilder().Build(AppCompatActivity, config);
    }


    protected virtual void SetAction(bool ok)
    {
        try
        {
            var txt = Dialog.FindViewById<TextView>(int.MaxValue);
            if (txt == null)
            {
                txt = Dialog.CurrentFocus as TextView;
                txt ??= Activity.FindViewById<TextView>(int.MaxValue);
            }
            Config?.OnAction(new PromptResult(ok, txt.Text.Trim()));
            Dismiss();
        }
        catch { } // swallow
    }
}
