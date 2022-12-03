using System;
using System.Collections.Generic;


namespace Acr.UserDialogs;


public class ActionSheetConfig : IAndroidStyleDialogConfig
{
    public static int? DefaultAndroidStyleId { get; set; }
    public static bool DefaultUseBottomSheet { get; set; }

    public static string DefaultCancelText { get; set; } = "Cancel";
    public static string DefaultDestructiveText { get; set; } = "Remove";
    public static string DefaultItemIcon { get; set; }


    public string Title { get; set; }
    public string Message { get; set; }
    public ActionSheetOption Cancel { get; set; }
    public ActionSheetOption Destructive { get; set; }
    public IList<ActionSheetOption> Options { get; set; } = new List<ActionSheetOption>();
    public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
    //public bool UwpCancelOnEscKey { get; set; }
    //public bool UwpSubmitOnEnterKey { get; set; }

    /// <summary>
    /// This only currently applies to android
    /// </summary>
    public bool UseBottomSheet { get; set; } = DefaultUseBottomSheet;

    /// <summary>
    /// This icon is applied to the list items, not to destructive or cancel
    /// </summary>
    public string ItemIcon { get; set; } = DefaultItemIcon;


    public ActionSheetConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public ActionSheetConfig SetUseBottomSheet(bool useBottomSheet)
    {
        UseBottomSheet = useBottomSheet;
        return this;
    }


    public ActionSheetConfig SetCancel(string text = null, Action action = null, string icon = null)
    {
        Cancel = new ActionSheetOption(text ?? DefaultCancelText, action, icon);
        return this;
    }


    public ActionSheetConfig SetDestructive(string text = null, Action action = null, string icon = null)
    {
        Destructive = new ActionSheetOption(text ?? DefaultDestructiveText, action, icon);
        return this;
    }


    public ActionSheetConfig SetMessage(string msg)
    {
        Message = msg;
        return this;
    }


    public ActionSheetConfig Add(string text, Action action = null, string icon = null)
    {
        Options.Add(new ActionSheetOption(text, action, icon));
        return this;
    }
}
