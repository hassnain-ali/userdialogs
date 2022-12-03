using System;
using System.Drawing;


namespace Acr.UserDialogs;

public class ToastAction
{
    public string Text { get; set; }
    public Color? TextColor { get; set; }
    public Action Action { get; set; }


    public ToastAction SetText(string text)
    {
        Text = text;
        return this;
    }


    public ToastAction SetTextColor(Color color)
    {
        TextColor = color;
        return this;
    }


    public ToastAction SetAction(Action action)
    {
        Action = action;
        return this;
    }
}
