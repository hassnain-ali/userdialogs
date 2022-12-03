using Acr.UserDialogs;

namespace Sample.ViewModels;

public class SpecificCasesViewModel : AbstractViewModel
{

    /* Unmerged change from project 'Sample (net6.0-ios)'
    Before:
            public IList<CommandViewModel> Commands { get; }


            public SpecificCasesViewModel(IUserDialogs dialogs) : base(dialogs)
            {
                this.Commands = new List<CommandViewModel>
    After:
            public Sample.ViewModels;

    private ist<CommandViewModel> Commands { get; }

    [Obsolete]
    public SpecificCasesViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        Commands = new List<CommandViewModel>
    */

    /* Unmerged change from project 'Sample (net6.0-android)'
    Before:
            public IList<CommandViewModel> Commands { get; }


            public SpecificCasesViewModel(IUserDialogs dialogs) : base(dialogs)
            {
                this.Commands = new List<CommandViewModel>
    After:
            public Sample.ViewModels;

    private ist<CommandViewModel> Commands { get; }

    [Obsolete]
    public SpecificCasesViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        Commands = new List<CommandViewModel>
    */
    public IList<CommandViewModel> Commands { get; }


    public SpecificCasesViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        Commands = new List<CommandViewModel>
        {
            new CommandViewModel
            {
                Text = "Loading Task to Alert",
                Command = new Command(() =>
                {
                    Dialogs.ShowLoading("You really shouldn't use ShowLoading");
                    _ = Task.Delay(TimeSpan.FromSeconds(2))
                        .ContinueWith(x => Dialogs.Alert("Do you see me?"));
                })
            },
            new CommandViewModel
            {
                Text = "Two Date Pickers",
                Command = new Command(async () =>
                {
                    var v1 = await Dialogs.DatePromptAsync("Date 1 (Past -1 Day)", DateTime.Now.AddDays(-1));
                    if (!v1.Ok)
                        return;

                    var v2 = await Dialogs.DatePromptAsync("Date 2 (Future +1 Day)", DateTime.Now.AddDays(1));
                    if (!v2.Ok)
                        return;

                    _ = Dialogs.Alert($"Date 1: {v1.SelectedDate} - Date 2: {v2.SelectedDate}");
                })
            },
            new CommandViewModel
            {
                Text = "Start Loading Twice",
                Command = new Command(async () =>
                {
                    Dialogs.ShowLoading("Loading 1");
                    await Task.Delay(1000);
                    Dialogs.ShowLoading("Loading 2");
                    await Task.Delay(1000);
                    Dialogs.HideLoading();
                })
            },
            new CommandViewModel
            {
                Text = "Async & OnAction Fail!",
                Command = new Command(async () =>
                {
                    try
                    {
                        await Dialogs.AlertAsync(new AlertConfig
                        {
                            OnAction = () => { }
                        });
                    }
                    catch
                    {
                        _ = Dialogs.Alert("It failed... GOOOD");
                    }
                })
            },
            new CommandViewModel
            {
                Text = "Toast from Background Thread",
                Command = new Command(() =>
                    Task.Factory.StartNew(() =>
                        Dialogs.Toast("Test From Background"),
                        TaskCreationOptions.LongRunning
                    )
                )
            },
            new CommandViewModel
            {
                Text = "Alert from Background Thread",
                Command = new Command(() =>
                    Task.Factory.StartNew(() =>
                        Dialogs.Alert("Test From Background"),
                        TaskCreationOptions.LongRunning
                    )
                )
            },
            new CommandViewModel
            {
                Text = "Two alerts with one Cancellation Token Source",
                Command = new Command(async () =>
                {
                    try
                    {
                        var cts = new CancellationTokenSource();

                        await Dialogs.AlertAsync("Press ok and then wait", "Hi", null, cts.Token);
                        cts.CancelAfter(TimeSpan.FromSeconds(3));
                        await Dialogs.AlertAsync("I'll close soon, just wait", "Hi", null, cts.Token);
                    }
                    catch(OperationCanceledException)
                    {
                    }
                })
            },
            new CommandViewModel
            {
                Text = "Large Toast Text",
                Command = new Command(() =>
                    Dialogs.Toast(
                        "This is a really long message to test text wrapping and other such things that are painful for toast dialogs to render fully in two line labels")
                )
            },
            new CommandViewModel
            {
                Text = "Toast with image",
                Command = new Command(() =>
                {
                    var img = Device.RuntimePlatform == Device.UWP ? "ms-appx:///Assets/emoji_cool_small.png" : "emoji_cool_small.png";
                    _ = Dialogs.Toast(new ToastConfig("Wow what a cool guy").SetIcon(img));
                })
            },
            new CommandViewModel
            {
                Text = "Toast (no action)",
                Command = new Command(() => Dialogs.Toast("TEST"))
            },
            new CommandViewModel
            {
                Text = "Prompt OnTextChanged with Initial Value",
                Command = new Command(async () =>
                {
                    _ = await Dialogs.PromptAsync(new PromptConfig()
                        .SetMessage("GOOD = ENABLED")
                        .SetText("GOOD")
                        .SetOnTextChanged(args =>
                            args.IsValid = args.Value.Equals("GOOD")
                        )
                    );
                    _ = await Dialogs.PromptAsync(new PromptConfig()
                        .SetMessage("GOOD = ENABLED")
                        .SetText("BAD")
                        .SetOnTextChanged(args =>
                            args.IsValid = args.Value.Equals("GOOD")
                        )
                    );
                    // TODO
                })
            }
        };
    }
}
