using System;


namespace Acr.UserDialogs;

public class ProgressDialogConfig
{
    public static string DefaultCancelText { get; set; } = "Cancel";
    public static string DefaultTitle { get; set; } = "Loading";
    public static MaskType DefaultMaskType { get; set; } = MaskType.Black;
    public static bool UseAndroidImmersiveMode { get; set; }


    public string CancelText { get; set; }
    public string Title { get; set; }
    public bool AutoShow { get; set; }
    public bool IsDeterministic { get; set; }
    public MaskType MaskType { get; set; }
    public Action OnCancel { get; set; }


    public ProgressDialogConfig()
    {
        Title = DefaultTitle;
        CancelText = DefaultCancelText;
        MaskType = DefaultMaskType;
        AutoShow = true;
    }


    public ProgressDialogConfig SetCancel(string cancelText = null, Action onCancel = null)
    {
        if (cancelText != null)
            CancelText = cancelText;

        OnCancel = onCancel;
        return this;
    }


    public ProgressDialogConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public ProgressDialogConfig SetMaskType(MaskType maskType)
    {
        MaskType = maskType;
        return this;
    }


    public ProgressDialogConfig SetAutoShow(bool autoShow)
    {
        AutoShow = autoShow;
        return this;
    }


    public ProgressDialogConfig SetIsDeterministic(bool isDeterministic)
    {
        IsDeterministic = isDeterministic;
        return this;
    }
}
