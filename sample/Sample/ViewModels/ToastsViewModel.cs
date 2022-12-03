using System.Windows.Input;
using Acr.UserDialogs;

namespace Sample.ViewModels;

public class ToastsViewModel : AbstractViewModel
{
    public ToastsViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        SecondsDuration = 3;

        ActionText = "Ok";
        Message = "This is a test of the emergency toast system";

        //this.ActionTextColor = ToHex(Color.White);
        //this.MessageTextColor = ToHex(Color.White);
        //this.BackgroundColor = ToHex(Color.Blue);

        Open = new Command(() =>
        {
            // var icon = await BitmapLoader.Current.LoadFromResource("emoji_cool_small.png", null, null);

            ToastConfig.DefaultBackgroundColor = System.Drawing.Color.AliceBlue;
            ToastConfig.DefaultMessageTextColor = System.Drawing.Color.Red;
            ToastConfig.DefaultActionTextColor = System.Drawing.Color.DarkRed;
            //var bgColor = FromHex(this.BackgroundColor);
            //var msgColor = FromHex(this.MessageTextColor);
            //var actionColor = FromHex(this.ActionTextColor);

            _ = dialogs.Toast(new ToastConfig(Message)
                //.SetBackgroundColor(bgColor)
                //.SetMessageTextColor(msgColor)
                .SetDuration(TimeSpan.FromSeconds(SecondsDuration))
                .SetPosition(ShowOnTop ? ToastPosition.Top : ToastPosition.Bottom)
                //.SetIcon(icon)
                .SetAction(x => x
                    .SetText(ActionText)
                    //.SetTextColor(actionColor)
                    .SetAction(() => dialogs.Alert("You clicked the primary toast button"))
                )
            );
        });
    }

    [Obsolete]
    private static System.Drawing.Color FromHex(string hex)
    {
        var c = Color.FromHex(hex);
        var dc = System.Drawing.Color.FromArgb((int)c.Alpha, (int)c.Red, (int)c.Green, (int)c.Blue);
        return dc;
    }

    private static string ToHex(Color color)
    {
        var red = (int)(color.Red * 255);
        var green = (int)(color.Green * 255);
        var blue = (int)(color.Blue * 255);
        //var alpha = (int)(color.A * 255);
        //var hex = String.Format($"#{red:X2}{green:X2}{blue:X2}{alpha:X2}");
        var hex = string.Format($"#{red:X2}{green:X2}{blue:X2}");
        return hex;
    }


    public ICommand Open { get; }

    private string backgroundColor;
    public string BackgroundColor
    {
        get { return backgroundColor; }
        set
        {
            if (backgroundColor == value)
                return;

            backgroundColor = value;
            OnPropertyChanged();
        }
    }

    private int secondsDuration;
    public int SecondsDuration
    {
        get { return secondsDuration; }
        set
        {
            if (secondsDuration == value)
                return;

            secondsDuration = value;
            OnPropertyChanged();
        }
    }

    private bool showOnTop;
    public bool ShowOnTop
    {
        get => showOnTop;
        set
        {
            if (showOnTop == value)
                return;

            showOnTop = value;
            OnPropertyChanged();
        }
    }

    private string actionText;
    public string ActionText
    {
        get { return actionText; }
        set
        {
            if (actionText == value)
                return;

            actionText = value;
            OnPropertyChanged();
        }
    }

    private string actionTextColor;
    public string ActionTextColor
    {
        get { return actionTextColor; }
        set
        {
            if (actionTextColor == value)
                return;

            actionTextColor = value;
            OnPropertyChanged();
        }
    }

    private string messageTextColor;
    public string MessageTextColor
    {
        get { return messageTextColor; }
        set
        {
            if (messageTextColor == value)
                return;

            messageTextColor = value;
            OnPropertyChanged();
        }
    }

    private string message;
    public string Message
    {
        get { return message; }
        set
        {
            if (message == value)
                return;

            message = value;
            OnPropertyChanged();
        }
    }
}
