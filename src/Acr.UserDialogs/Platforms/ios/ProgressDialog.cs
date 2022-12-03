#if __IOS__
using BigTed;
#endif
using UIKit;


namespace Acr.UserDialogs;


public class ProgressDialog : IProgressDialog
{
    private readonly ProgressDialogConfig config;


    public ProgressDialog(ProgressDialogConfig config)
    {
        this.config = config;
        title = config.Title;
    }


    #region IProgressDialog Members

    private string title;
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


    public virtual void Show()
    {
        IsShowing = true;
        Refresh();
    }


    public virtual void Hide()
    {
        IsShowing = false;
#if __IOS__
        UIApplication.SharedApplication.InvokeOnMainThread(BTProgressHUD.Dismiss);
#endif
    }

    #endregion

    #region IDisposable Members

    public virtual void Dispose()
    {
        Hide();
    }

    #endregion

    #region Internals

    protected virtual void Refresh()
    {
        if (!IsShowing)
            return;

        var txt = Title;
        float p = -1;
        if (config.IsDeterministic)
        {
            p = (float)PercentComplete / 100;
            if (!string.IsNullOrWhiteSpace(txt))
                txt += "... ";
            txt += PercentComplete + "%";
        }

        UIApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            if (config.OnCancel == null)
#if __IOS__
                BTProgressHUD.Show(
                    Title,
                    p,
                    config.MaskType.ToNative()
                );
            else
                BTProgressHUD.Show(
                    config.CancelText,
                    config.OnCancel,
                    txt,
                    p,
                    config.MaskType.ToNative()
                );
#endif
        });
    }
    #endregion

}
