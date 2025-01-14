﻿using System;


namespace Acr.UserDialogs;


public class ConfirmConfig : IStandardDialogConfig, IAndroidStyleDialogConfig
{
    public static bool DefaultUseYesNo { get; set; }
    public static string DefaultYes { get; set; } = "Yes";
    public static string DefaultNo { get; set; } = "No";
    public static string DefaultOkText { get; set; } = "Ok";
    public static string DefaultCancelText { get; set; } = "Cancel";
    public static int? DefaultAndroidStyleId { get; set; }


    public string Title { get; set; }
    public string Message { get; set; }
    public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
    public Action<bool> OnAction { get; set; }
    //public bool UwpCancelOnEscKey { get; set; }
    //public bool UwpSubmitOnEnterKey { get; set; }

    public string OkText { get; set; } = !DefaultUseYesNo ? DefaultOkText : DefaultYes;
    public string CancelText { get; set; } = !DefaultUseYesNo ? DefaultCancelText : DefaultNo;


    public ConfirmConfig UseYesNo()
    {
        OkText = DefaultYes;
        CancelText = DefaultNo;
        return this;
    }


    public ConfirmConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public ConfirmConfig SetMessage(string message)
    {
        Message = message;
        return this;
    }


    public ConfirmConfig SetOkText(string text)
    {
        OkText = text;
        return this;
    }


    public ConfirmConfig SetAction(Action<bool> action)
    {
        OnAction = action;
        return this;
    }


    public ConfirmConfig SetCancelText(string text)
    {
        CancelText = text;
        return this;
    }
}
