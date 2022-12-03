using System;


namespace Acr.UserDialogs;


public class AlertConfig : IStandardDialogConfig, IAndroidStyleDialogConfig
{
    public static string DefaultOkText { get; set; } = "Ok";
    public static int? DefaultAndroidStyleId { get; set; }

    public string OkText { get; set; } = DefaultOkText;
    public string Title { get; set; }
    public string Message { get; set; }
    public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
    public Action OnAction { get; set; }
    //public bool UwpCancelOnEscKey { get; set; }
    //public bool UwpSubmitOnEnterKey { get; set; }


    public AlertConfig SetOkText(string text)
    {
        OkText = text;
        return this;
    }


    public AlertConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public AlertConfig SetMessage(string message)
    {
        Message = message;
        return this;
    }


    public AlertConfig SetAction(Action action)
    {
        OnAction = action;
        return this;
    }
}
