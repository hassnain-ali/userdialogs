using System;
using Android.App;
using Android.Views;
using AndroidHUD;


namespace Acr.UserDialogs;

public class ProgressDialog : IProgressDialog
{
    private readonly Activity activity;
    private readonly ProgressDialogConfig config;


    public ProgressDialog(ProgressDialogConfig config, Activity activity)
    {
        this.config = config;
        this.activity = activity;
    }

    #region IProgressDialog Members

    private string title;

    [Obsolete]
    public virtual string Title
    {
        get { return title; }
        set
        {
            if (title == value)
                return;

            title = value;
            Refresh();
        }
    }

    private int percentComplete;

    [Obsolete]
    public virtual int PercentComplete
    {
        get { return percentComplete; }
        set
        {
            if (percentComplete == value)
                return;

            percentComplete = value > 100 ? 100 : value < 0 ? 0 : value;

            Refresh();
        }
    }


    public virtual bool IsShowing { get; private set; }

    [Obsolete]
    public virtual void Show()
    {
        if (IsShowing)
            return;

        IsShowing = true;
        Refresh();
    }


    public virtual void Hide()
    {
        IsShowing = false;
        try
        {
            AndHUD.Shared.Dismiss(activity);
        }
        catch (Exception exc)
        {
            Infrastructure.Log.Error("Dismiss", $"Exception ({exc.GetType().FullName}) occured while dismissing dialog: {exc.Message}");
        }
    }

    #endregion

    #region IDisposable Members

    public virtual void Dispose()
    {
        Hide();
    }

    #endregion

    #region Internals

    [Obsolete]
    protected virtual void Refresh()
    {
        if (!IsShowing)
            return;

        var p = -1;
        var txt = Title;
        if (config.IsDeterministic)
        {
            p = PercentComplete;
            if (!string.IsNullOrWhiteSpace(txt))
                txt += "\n";

            txt += p + "%\n";
        }

        if (config.OnCancel != null)
            txt += "\n" + config.CancelText;

        AndHUD.Shared.Show(
            activity,
            txt,
            p,
            config.MaskType.ToNative(),
            null,
            OnCancelClick,
            true,
            null,
            BeforeShow,
            AfterShow
        );
    }

    private void BeforeShow(Dialog dialog)
    {
        if (dialog == null)
            return;
        dialog.Window.AddFlags(WindowManagerFlags.NotFocusable);
    }

    [Obsolete]
    private void AfterShow(Dialog dialog)
    {
        if (dialog == null)
            return;

        //Maintain Immersive mode
        if (ProgressDialogConfig.UseAndroidImmersiveMode)
            dialog.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                                                    SystemUiFlags.ImmersiveSticky |
                                                    SystemUiFlags.LayoutStable |
                                                    SystemUiFlags.LayoutFullscreen |
                                                    SystemUiFlags.LayoutHideNavigation |
                                                    SystemUiFlags.HideNavigation |
                                                    SystemUiFlags.Fullscreen);

        dialog.Window.ClearFlags(WindowManagerFlags.NotFocusable);
    }

    private void OnCancelClick()
    {
        if (config.OnCancel == null)
            return;

        Hide();
        config.OnCancel();
    }

    #endregion
}
