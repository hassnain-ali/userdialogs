﻿using System;


namespace Acr.UserDialogs;


public class PromptConfig : IStandardDialogConfig, IAndroidStyleDialogConfig
{
    public static string DefaultOkText { get; set; } = "Ok";
    public static string DefaultCancelText { get; set; } = "Cancel";
    public static int? DefaultAndroidStyleId { get; set; }
    public static int? DefaultMaxLength { get; set; }

    public string Title { get; set; }
    public string Message { get; set; }
    public Action<PromptResult> OnAction { get; set; }

    public bool IsCancellable { get; set; } = true;
    public string Text { get; set; }

    public string OkText { get; set; } = DefaultOkText;
    public string CancelText { get; set; } = DefaultCancelText;
    public string Placeholder { get; set; }
    public int? MaxLength { get; set; } = DefaultMaxLength;
    public int? AndroidStyleId { get; set; } = DefaultAndroidStyleId;
    public InputType InputType { get; set; } = InputType.Default;
    public AutoCorrectionConfig AutoCorrectionConfig { get; set; } = AutoCorrectionConfig.Default;
    //public bool UwpCancelOnEscKey { get; set; }
    //public bool UwpSubmitOnEnterKey { get; set; }
    public Action<PromptTextChangedArgs> OnTextChanged { get; set; }


    public PromptConfig SetAction(Action<PromptResult> action)
    {
        OnAction = action;
        return this;
    }


    public PromptConfig SetTitle(string title)
    {
        Title = title;
        return this;
    }


    public PromptConfig SetMessage(string message)
    {
        Message = message;
        return this;
    }


    public PromptConfig SetCancellable(bool cancel)
    {
        IsCancellable = cancel;
        return this;
    }


    public PromptConfig SetOkText(string text)
    {
        OkText = text;
        return this;
    }


    public PromptConfig SetMaxLength(int maxLength)
    {
        MaxLength = maxLength;
        return this;
    }


    public PromptConfig SetText(string text)
    {
        Text = text;
        return this;
    }


    public PromptConfig SetCancelText(string cancelText)
    {
        IsCancellable = true;
        CancelText = cancelText;
        return this;
    }


    public PromptConfig SetPlaceholder(string placeholder)
    {
        Placeholder = placeholder;
        return this;
    }


    public PromptConfig SetInputMode(InputType inputType)
    {
        InputType = inputType;
        return this;
    }


    public PromptConfig SetOnTextChanged(Action<PromptTextChangedArgs> onChange)
    {
        OnTextChanged = onChange;
        return this;
    }
}
