using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
#if ANDROIDX
using AndroidX.AppCompat.App;
#else
using Android.Support.V7.App;
#endif

namespace Acr.UserDialogs.Fragments;

public abstract class AbstractAppCompatDialogFragment<T> : AppCompatDialogFragment where T : class
{
    public T Config { get; set; }


    public override void OnSaveInstanceState(Bundle bundle)
    {
        base.OnSaveInstanceState(bundle);
        ConfigStore.Instance.Store(bundle, Config);
    }


    public override Dialog OnCreateDialog(Bundle bundle)
    {
        Dialog dialog = null;
        if (Config == null && !ConfigStore.Instance.Contains(bundle))
        {
            ShowsDialog = false;
            Dismiss();
        }
        else
        {
            Config ??= ConfigStore.Instance.Pop<T>(bundle);
            dialog = CreateDialog(Config);
            SetDialogDefaults(dialog);
        }
        return dialog;
    }


    protected virtual void SetDialogDefaults(Dialog dialog)
    {
        dialog.Window.SetSoftInputMode(SoftInput.StateVisible);
        dialog.SetCancelable(false);
        dialog.SetCanceledOnTouchOutside(false);
        dialog.KeyPress += OnKeyPress;
        // TODO: fix for immersive mode - http://stackoverflow.com/questions/22794049/how-to-maintain-the-immersive-mode-in-dialogs/23207365#23207365
        //dialog.getWindow().setFlags(WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE, WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE);
    }


    public override void OnDetach()
    {
        base.OnDetach();
        if (Dialog != null)
            Dialog.KeyPress -= OnKeyPress;
    }


    protected virtual void OnKeyPress(object sender, DialogKeyEventArgs args)
    {
    }


    protected abstract Dialog CreateDialog(T config);
    protected AppCompatActivity AppCompatActivity => Activity as AppCompatActivity;
}
