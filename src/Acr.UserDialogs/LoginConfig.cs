﻿using System;


namespace Acr.UserDialogs;


public class LoginConfig : IStandardDialogConfig, IAndroidStyleDialogConfig
{
    public static string DefaultTitle { get; set; } = "Login";
    public static string DefaultOkText { get; set; } = "Ok";
    public static string DefaultCancelText { get; set; } = "Cancel";
    public static string DefaultLoginPlaceholder { get; set; } = "User Name";
    public static string DefaultPasswordPlaceholder { get; set; } = "Password";
    public static int? DefaultAndroidStyleId { get; set; }

    public string Title { get; set; } = DefaultTitle;
    public string Message { get; set; }
    public string OkText { get; set; } = DefaultOkText;
    public string CancelText { get; set; } = DefaultCancelText;
    public string LoginValue { get; set; }
    public string LoginPlaceholder { get; set; } = DefaultLoginPlaceholder;
    public string PasswordPlaceholder { get; set; } = DefaultPasswordPlaceholder;
    public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
    //public bool UwpCancelOnEscKey { get; set; }
    //public bool UwpSubmitOnEnterKey { get; set; }
    public Action<LoginResult> OnAction { get; set; }


    public LoginConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public LoginConfig SetMessage(string msg)
    {
        Message = msg;
        return this;
    }


    public LoginConfig SetOkText(string ok)
    {
        OkText = ok;
        return this;
    }


    public LoginConfig SetCancelText(string cancel)
    {
        CancelText = cancel;
        return this;
    }


    public LoginConfig SetLoginValue(string txt)
    {
        LoginValue = txt;
        return this;
    }


    public LoginConfig SetLoginPlaceholder(string txt)
    {
        LoginPlaceholder = txt;
        return this;
    }


    public LoginConfig SetPasswordPlaceholder(string txt)
    {
        PasswordPlaceholder = txt;
        return this;
    }


    public LoginConfig SetAction(Action<LoginResult> action)
    {
        OnAction = action;
        return this;
    }
}
