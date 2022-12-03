using System.Windows.Input;
using Acr.UserDialogs;

namespace Sample.ViewModels;

public class StandardViewModel : AbstractViewModel
{
    public IList<CommandViewModel> Commands { get; } = new List<CommandViewModel>();


    public StandardViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        Commands = new List<CommandViewModel>
        {
            new CommandViewModel
            {
                Text = "Alert",
                Command = Create(async token => await Dialogs.AlertAsync("Test alert", "Alert Title", null, token))
            },
            new CommandViewModel
            {
                Text = "Alert Long Text",
                Command = Create(async token =>
                    await Dialogs.AlertAsync(
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc consequat diam nec eros ornare, vitae cursus nunc molestie. Praesent eget lacus non neque cursus posuere. Nunc venenatis quam sed justo bibendum, ut convallis arcu lobortis. Vestibulum in diam nisl. Nulla pulvinar lacus vel laoreet auctor. Morbi mi urna, viverra et accumsan in, pretium vel lorem. Proin elementum viverra commodo. Sed nunc justo, sollicitudin eu fermentum vitae, faucibus a est. Nulla ante turpis, iaculis et magna sed, facilisis blandit dolor. Morbi est felis, semper non turpis non, tincidunt consectetur enim.",
                        cancelToken: token
                    )
                )
            },
            new CommandViewModel
            {
                Text = "Action Sheet",
                Command = CreateActionSheetCommand(false, true, 6)
            },
            new CommandViewModel
            {
                Text = "Action Sheet /w Message",
                Command = CreateActionSheetCommand(false, false, 6, "This is an example of using a message in Acr.UserDialogs actionsheets.  I needed a long message here!")
            },
            new CommandViewModel
            {
                Text = "Action Sheet (No Cancel)",
                Command = CreateActionSheetCommand(false, false, 3)
            },
            new CommandViewModel
            {
                Text = "Action Sheet (async)",
                Command = Create(async token =>
                {
                    var result = await Dialogs.ActionSheetAsync("Test Title", "Cancel", "Destroy", token, "Button1", "Button2", "Button3");
                    Result(result);
                })
            },
            new CommandViewModel
            {
                Text = "Bottom Sheet (Android Only)",
                Command = CreateActionSheetCommand(true, true, 6)
            },
            new CommandViewModel
            {
                Text = "Confirm",
                Command = Create(async token =>
                {
                    var r = await Dialogs.ConfirmAsync("Pick a choice", "Pick Title", cancelToken: token);
                    var text = r ? "Yes" : "No";
                    Result($"Confirmation Choice: {text}");
                })
            },
            new CommandViewModel
            {
                Text = "Login",
                Command = Create(async token =>
                {
                    var r = await Dialogs.LoginAsync(new LoginConfig
                    {
                        //LoginValue = "LastUserName",
                        Message = "DANGER",
                        OkText = "DO IT",
                        CancelText = "GET OUT",
                        LoginPlaceholder = "Username Placeholder",
                        PasswordPlaceholder = "Password Placeholder"
                    }, token);
                    var status = r.Ok ? "Success" : "Cancelled";
                    Result($"Login {status} - User Name: {r.LoginText} - Password: {r.Password}");
                })
            },
            new CommandViewModel
            {
                Text = "Prompt",
                Command = new Command(() => Dialogs.ActionSheet(new ActionSheetConfig()
                    .SetTitle("Choose Type")
                    .Add("Default", () => PromptCommand(InputType.Default))
                    .Add("E-Mail", () => PromptCommand(InputType.Email))
                    .Add("Name", () => PromptCommand(InputType.Name))
                    .Add("Number", () => PromptCommand(InputType.Number))
                    .Add("Number with Decimal", () => PromptCommand(InputType.DecimalNumber))
                    .Add("Password", () => PromptCommand(InputType.Password))
                    .Add("Numeric Password (PIN)", () => PromptCommand(InputType.NumericPassword))
                    .Add("Phone", () => PromptCommand(InputType.Phone))
                    .Add("Url", () => PromptCommand(InputType.Url))
                    .SetCancel()
                ))
            },
            new CommandViewModel
            {
                Text = "Prompt Max Length",
                Command = Create(async token =>
                {
                    var result = await Dialogs.PromptAsync(new PromptConfig()

                        .SetTitle("Max Length Prompt")
                        .SetPlaceholder("Maximum Text Length (10)")
                        .SetInputMode(InputType.Name)
                        .SetMaxLength(10), token);

                    Result($"Result - {result.Ok} - {result.Text}");
                })
            },
            new CommandViewModel
            {
                Text = "Prompt (No Text or Cancel)",
                Command = Create(async token =>
                {
                    var result = await Dialogs.PromptAsync(new PromptConfig
                    {
                        Title = "PromptWithTextAndNoCancel",
                        Text = "Existing Text",
                        IsCancellable = false
                    }, token);
                    Result($"Result - {result.Ok} - {result.Text}");
                })
            },
            new CommandViewModel
            {
                Text = "Prompt Text Validate",
                Command = new Command(() => Dialogs.Prompt(new PromptConfig
                    {
                        Title = "Prompt Text Validate",
                        Message = "You must type the word \"yes\" to enable OK button",
                        OnTextChanged = args => args.IsValid = args.Value.Equals("yes", StringComparison.CurrentCultureIgnoreCase),
                        OnAction = (result) => Result($"Result - {result.Ok} - {result.Text}")                        }))
            },
            new CommandViewModel
            {
                Text = "Prompt Text Format",
                Command = new Command(async () =>
                {
                    var result = await Dialogs.PromptAsync(new PromptConfig
                    {
                        Title = "Prompt Text Format",
                        Message = "Type in lower case and it will convert to upper case",
                        OnTextChanged = args => args.Value = args.Value.ToUpper()
                    });
                    Result($"Result - {result.Ok} - {result.Text}");
                })
            },
            new CommandViewModel
            {
                Text = "Prompt NoAutoCorrect",
                Command = new Command(async () =>
                {
                    var result = await Dialogs.PromptAsync(new PromptConfig
                    {
                        Title = "Prompt NoAutoCorrect",
                        Message = "When entering text, the keyboard should not provide autocorrect options",
                        AutoCorrectionConfig = AutoCorrectionConfig.No
                    });
                })
            },
            new CommandViewModel
            {
                Text = "Prompt AutoCorrect",
                Command = new Command(async () =>
                {
                    var result = await Dialogs.PromptAsync(new PromptConfig
                    {
                        Title = "Prompt AutoCorrect",
                        Message = "When entering text, the keyboard should provide autocorrect options",
                        AutoCorrectionConfig = AutoCorrectionConfig.Yes
                    });
                })
            },
            new CommandViewModel
            {
                Text = "Date",
                Command = Create(async token =>
                {
                    var result = await Dialogs.DatePromptAsync(new DatePromptConfig
                    {
                        IsCancellable = true,
                        MinimumDate = DateTime.Now.AddDays(-3),
                        MaximumDate = DateTime.Now.AddDays(1)
                    }, token);
                    Result($"Date Prompt: {result.Ok} - Value: {result.SelectedDate}");
                })
            },
            new CommandViewModel
            {
                Text = "Time",
                Command = Create(async token =>
                {
                    var result = await Dialogs.TimePromptAsync(new TimePromptConfig
                    {
                        IsCancellable = true
                    }, token);
                    Result($"Time Prompt: {result.Ok} - Value: {result.SelectedTime}");
                })
            },
            new CommandViewModel
            {
                Text = "Time (24 hour clock)",
                Command = Create (async token => {
                    var result = await Dialogs.TimePromptAsync(new TimePromptConfig {
                        IsCancellable = true,
                        Use24HourClock = true
                    }, token);
                    Result ($"Time Prompt: {result.Ok} - Value: {result.SelectedTime}");
                })
            }
        };
    }

    private CancellationTokenSource cancelSrc;
    private bool autoCancel;
    public bool AutoCancel
    {
        get { return autoCancel; }
        set
        {
            if (autoCancel == value)
                return;

            autoCancel = value;
            OnPropertyChanged();
            cancelSrc = value ? new CancellationTokenSource() : null;
        }
    }
    /* Unmerged change from project 'Sample (net6.0-ios)'
    Before:
            ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null)
            {
                return new Command(() =>
                {
                    var cfg = new ActionSheetConfig()
                        .SetTitle("Test Title")
                        .SetMessage(message)
                        .SetUseBottomSheet(useBottomSheet);
    After:
            ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null) => new Command(() =>
                                                                                                                      {
                                                                                                                          var cfg = new ActionSheetConfig()
                                                                                                                              .SetTitle("Test Title")
                                                                                                                              .SetMessage(message)
                                                                                                                              .SetUseBottomSheet(useBottomSheet);
    */

    /* Unmerged change from project 'Sample (net6.0-android)'
    Before:
            ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null)
            {
                return new Command(() =>
                {
                    var cfg = new ActionSheetConfig()
                        .SetTitle("Test Title")
                        .SetMessage(message)
                        .SetUseBottomSheet(useBottomSheet);
    After:
            ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null) => new Command(() =>
                                                                                                                      {
                                                                                                                          var cfg = new ActionSheetConfig()
                                                                                                                              .SetTitle("Test Title")
                                                                                                                              .SetMessage(message)
                                                                                                                              .SetUseBottomSheet(useBottomSheet);
    */


    private ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null) => new Command(() =>
                                                                                                                      {
                                                                                                                          var cfg = new ActionSheetConfig()
                                                                                                                              .SetTitle("Test Title")
                                                                                                                              .SetMessage(message)
                                                                                                                              .SetUseBottomSheet(useBottomSheet);


                                                                                                                          /* Unmerged change from project 'Sample (net6.0-ios)'
                                                                                                                          Before:
                                                                                                                                          for (var i = 0; i < items; i++)
                                                                                                                                          {
                                                                                                                                              var display = i + 1;
                                                                                                                                              cfg.Add(
                                                                                                                                                  "Option " + display,
                                                                                                                                                  () => this.Result($"Option {display} Selected"),
                                                                                                                                                  "icon.png"
                                                                                                                                              );
                                                                                                                                          }
                                                                                                                                          cfg.SetDestructive(null, () => this.Result("Destructive BOOM Selected"), "icon.png");
                                                                                                                                          if (cancel)
                                                                                                                                              cfg.SetCancel(null, () => this.Result("Cancel Selected"), "icon.png");
                                                                                                                          After:
                                                                                                                                                                                                                                                for (var i = 0; i < items; i++)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    var display = i + 1;
                                                                                                                                                                                                                                                    _ = cfg.Add(
                                                                                                                                                                                                                                                        "Option " + display,
                                                                                                                                                                                                                                                        () => this.Result($"Option {display} Selected"),
                                                                                                                                                                                                                                                        "icon.png"
                                                                                                                                                                                                                                                    );
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                _ = cfg.SetDestructive(null, () => this.Result("Destructive BOOM Selected"), "icon.png");
                                                                                                                                                                                                                                                if (cancel)
                                                                                                                                                                                                                                                    _ = cfg.SetCancel(null, () => this.Result("Cancel Selected"), "icon.png");
                                                                                                                          */

                                                                                                                          /* Unmerged change from project 'Sample (net6.0-android)'
                                                                                                                          Before:
                                                                                                                                          for (var i = 0; i < items; i++)
                                                                                                                                          {
                                                                                                                                              var display = i + 1;
                                                                                                                                              cfg.Add(
                                                                                                                                                  "Option " + display,
                                                                                                                                                  () => this.Result($"Option {display} Selected"),
                                                                                                                                                  "icon.png"
                                                                                                                                              );
                                                                                                                                          }
                                                                                                                                          cfg.SetDestructive(null, () => this.Result("Destructive BOOM Selected"), "icon.png");
                                                                                                                                          if (cancel)
                                                                                                                                              cfg.SetCancel(null, () => this.Result("Cancel Selected"), "icon.png");
                                                                                                                          After:
                                                                                                                                                                                                                                                for (var i = 0; i < items; i++)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    var display = i + 1;
                                                                                                                                                                                                                                                    _ = cfg.Add(
                                                                                                                                                                                                                                                        "Option " + display,
                                                                                                                                                                                                                                                        () => this.Result($"Option {display} Selected"),
                                                                                                                                                                                                                                                        "icon.png"
                                                                                                                                                                                                                                                    );
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                _ = cfg.SetDestructive(null, () => this.Result("Destructive BOOM Selected"), "icon.png");
                                                                                                                                                                                                                                                if (cancel)
                                                                                                                                                                                                                                                    _ = cfg.SetCancel(null, () => this.Result("Cancel Selected"), "icon.png");
                                                                                                                          */
                                                                                                                          for (var i = 0; i < items; i++)
                                                                                                                          {
                                                                                                                              var display = i + 1;
                                                                                                                              _ = cfg.Add(
                                                                                                                                  "Option " + display,
                                                                                                                                  () => Result($"Option {display} Selected"),
                                                                                                                                  "icon.png"
                                                                                                                              );
                                                                                                                          }
                                                                                                                          _ = cfg.SetDestructive(null, () => Result("Destructive BOOM Selected"), "icon.png");
                                                                                                                          if (cancel)
                                                                                                                              _ = cfg.SetCancel(null, () => Result("Cancel Selected"), "icon.png");


                                                                                                                          /* Unmerged change from project 'Sample (net6.0-ios)'
                                                                                                                          Before:
                                                                                                                                          var disp = this.Dialogs.ActionSheet(cfg);
                                                                                                                                          if (this.AutoCancel)
                                                                                                                                          {
                                                                                                                                              Task.Delay(TimeSpan.FromSeconds(3))
                                                                                                                                                  .ContinueWith(x => disp.Dispose());
                                                                                                                                          }
                                                                                                                                      });
                                                                                                                                  }


                                                                                                                                  ICommand Create(Func<CancellationToken?, Task> action)
                                                                                                                                  {
                                                                                                                                      return new Command(async () =>
                                                                                                                                      {
                                                                                                                                          try
                                                                                                                                          {
                                                                                                                                              this.cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
                                                                                                                                              await action(this.cancelSrc?.Token);
                                                                                                                                          }
                                                                                                                                          catch (OperationCanceledException)
                                                                                                                                          {
                                                                                                                                              if (this.AutoCancel)
                                                                                                                                                  this.cancelSrc = new CancellationTokenSource();
                                                                                                                          After:
                                                                                                                                                                                                                                                var disp = this.Dialogs.ActionSheet(cfg);
                                                                                                                                                                                                                                                if (this.AutoCancel)
                                                                                                                                                                                                                                                    _ = Task.Delay(TimeSpan.FromSeconds(3))
                                                                                                                                                                                                                                                        .ContinueWith(x => disp.Dispose());
                                                                                                                                                                                                                                            });

                                                                                                                          private ICommand Create(Func<CancellationToken?, Task> action) => new Command(async () =>
                                                                                                                                                                                             {
                                                                                                                                                                                                 try
                                                                                                                                                                                                 {
                                                                                                                                                                                                     cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
                                                                                                                                                                                                     await action(this.cancelSrc?.Token);
                                                                                                                                                                                                 }
                                                                                                                                                                                                 catch (OperationCanceledException)
                                                                                                                                                                                                 {
                                                                                                                                                                                                     if (this.AutoCancel)
                                                                                                                                                                                                         cancelSrc = new CancellationTokenSource();
                                                                                                                          */

                                                                                                                          /* Unmerged change from project 'Sample (net6.0-android)'
                                                                                                                          Before:
                                                                                                                                          var disp = this.Dialogs.ActionSheet(cfg);
                                                                                                                                          if (this.AutoCancel)
                                                                                                                                          {
                                                                                                                                              Task.Delay(TimeSpan.FromSeconds(3))
                                                                                                                                                  .ContinueWith(x => disp.Dispose());
                                                                                                                                          }
                                                                                                                                      });
                                                                                                                                  }


                                                                                                                                  ICommand Create(Func<CancellationToken?, Task> action)
                                                                                                                                  {
                                                                                                                                      return new Command(async () =>
                                                                                                                                      {
                                                                                                                                          try
                                                                                                                                          {
                                                                                                                                              this.cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
                                                                                                                                              await action(this.cancelSrc?.Token);
                                                                                                                                          }
                                                                                                                                          catch (OperationCanceledException)
                                                                                                                                          {
                                                                                                                                              if (this.AutoCancel)
                                                                                                                                                  this.cancelSrc = new CancellationTokenSource();
                                                                                                                          After:
                                                                                                                                                                                                                                                var disp = this.Dialogs.ActionSheet(cfg);
                                                                                                                                                                                                                                                if (this.AutoCancel)
                                                                                                                                                                                                                                                    _ = Task.Delay(TimeSpan.FromSeconds(3))
                                                                                                                                                                                                                                                        .ContinueWith(x => disp.Dispose());
                                                                                                                                                                                                                                            });

                                                                                                                          private ICommand Create(Func<CancellationToken?, Task> action) => new Command(async () =>
                                                                                                                                                                                             {
                                                                                                                                                                                                 try
                                                                                                                                                                                                 {
                                                                                                                                                                                                     cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
                                                                                                                                                                                                     await action(this.cancelSrc?.Token);
                                                                                                                                                                                                 }
                                                                                                                                                                                                 catch (OperationCanceledException)
                                                                                                                                                                                                 {
                                                                                                                                                                                                     if (this.AutoCancel)
                                                                                                                                                                                                         cancelSrc = new CancellationTokenSource();
                                                                                                                          */
                                                                                                                          var disp = Dialogs.ActionSheet(cfg);
                                                                                                                          if (AutoCancel)
                                                                                                                              _ = Task.Delay(TimeSpan.FromSeconds(3))
                                                                                                                                  .ContinueWith(x => disp.Dispose());
                                                                                                                      });

    private ICommand Create(Func<CancellationToken?, Task> action) => new Command(async () =>
                                                                       {
                                                                           try
                                                                           {
                                                                               cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
                                                                               await action(cancelSrc?.Token);
                                                                           }
                                                                           catch (OperationCanceledException)
                                                                           {
                                                                               if (AutoCancel)
                                                                                   cancelSrc = new CancellationTokenSource();


                                                                               /* Unmerged change from project 'Sample (net6.0-ios)'
                                                                               Before:
                                                                                                   this.Dialogs.Alert("Task cancelled successfully");
                                                                                               }
                                                                                           });
                                                                                       }


                                                                                       async Task PromptCommand(InputType inputType)
                                                                               After:
                                                                                                                                                          _ = Dialogs.Alert("Task cancelled successfully");
                                                                                                                                                      }
                                                                                                                                                  });

                                                                               private async Task PromptCommand(InputType inputType)
                                                                               */

                                                                               /* Unmerged change from project 'Sample (net6.0-android)'
                                                                               Before:
                                                                                                   this.Dialogs.Alert("Task cancelled successfully");
                                                                                               }
                                                                                           });
                                                                                       }


                                                                                       async Task PromptCommand(InputType inputType)
                                                                               After:
                                                                                                                                                          _ = Dialogs.Alert("Task cancelled successfully");
                                                                                                                                                      }
                                                                                                                                                  });

                                                                               private async Task PromptCommand(InputType inputType)
                                                                               */
                                                                               _ = Dialogs.Alert("Task cancelled successfully");
                                                                           }
                                                                       });

    private async Task PromptCommand(InputType inputType)
    {
        var msg = $"Enter a {inputType.ToString().ToUpper()} value";
        cancelSrc?.CancelAfter(TimeSpan.FromSeconds(3));
        var r = await Dialogs.PromptAsync(msg, inputType: inputType, cancelToken: cancelSrc?.Token);
        await Task.Delay(500);
        Result(r.Ok
            ? "OK " + r.Text
            : "Prompt Cancelled");
    }
}
