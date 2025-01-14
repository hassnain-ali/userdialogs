﻿using System;


namespace Acr.UserDialogs;


public class ActionSheetOption
{

    public string Text { get; set; }
    public Action Action { get; set; }
    public string ItemIcon { get; set; }


    public ActionSheetOption(string text, Action action = null, string icon = null)
    {
        Text = text;
        Action = action;
        ItemIcon = icon;
    }
}
