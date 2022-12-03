using System.Windows.Input;
using Acr.UserDialogs;
using Sample.ViewModels;

namespace Sample.ViewModels;

public class ProgressViewModel : AbstractViewModel
{
    public List<CommandViewModel> Commands { get; } = new List<CommandViewModel>();


    public ProgressViewModel(IUserDialogs dialogs) : base(dialogs)
    {
        Commands = new List<CommandViewModel>
        {
            new CommandViewModel
            {
                Text = "Loading",
                Command = LoadingCommand(MaskType.Black)
            },
            new CommandViewModel
            {
                Text = "Loading (Clear)",
                Command = LoadingCommand(MaskType.Clear)
            },
            new CommandViewModel
            {
                Text = "Loading (Gradient)",
                Command = LoadingCommand(MaskType.Gradient)
            },
            new CommandViewModel
            {
                Text = "Loading (None)",
                Command = LoadingCommand(MaskType.None)
            },
            new CommandViewModel
            {
                Text = "Progress",
                Command = new Command(async () =>
                {
                    var cancelled = false;

                    using (var dlg = Dialogs.Progress("Test Progress", () => cancelled = true))
                        while (!cancelled && dlg.PercentComplete < 100)
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(500));
                            dlg.PercentComplete += 2;
                        }
                    Result(cancelled ? "Progress Cancelled" : "Progress Complete");
                })
            },
            new CommandViewModel
            {
                Text = "Progress (No Cancel)",
                Command = new Command(async () =>
                {
                    using var dlg = Dialogs.Progress("Progress (No Cancel)");
                        while (dlg.PercentComplete < 100)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(1));
                            dlg.PercentComplete += 20;
                        }
                })
            },
            new CommandViewModel
            {
                Text = "Loading (No Cancel)",
                Command = new Command(async () =>
                {
                    using (Dialogs.Loading("Loading (No Cancel)"))
                        await Task.Delay(TimeSpan.FromSeconds(3));
                })
            },
            new CommandViewModel
            {
                Text = "Loading To Success",
                Command = new Command(async () =>
                {
                    using (Dialogs.Loading("Test Loading"))
                        await Task.Delay(3000);
                })
            },
            new CommandViewModel
            {
                Text = "Manual Loading",
                Command = new Command(async () =>
                {
                    Dialogs.ShowLoading("Manual Loading");
                    await Task.Delay(3000);
                    Dialogs.HideLoading();
                })
            }
        };
    }
    /* Unmerged change from project 'Sample (net6.0-ios)'
    Before:
            ICommand LoadingCommand(MaskType mask)
            {
                return new Command(async () =>
                {
                    var cancelSrc = new CancellationTokenSource();
                    var config = new ProgressDialogConfig()
                        .SetTitle("Loading")
                        .SetIsDeterministic(false)
                        .SetMaskType(mask)
                        .SetCancel(onCancel: cancelSrc.Cancel);
    After:
            ICommand LoadingCommand(MaskType mask) => new Command(async () =>
                                                       {
                                                           var cancelSrc = new CancellationTokenSource();
                                                           var config = new ProgressDialogConfig()
                                                               .SetTitle("Loading")
                                                               .SetIsDeterministic(false)
                                                               .SetMaskType(mask)
                                                               .SetCancel(onCancel: cancelSrc.Cancel);
    */

    /* Unmerged change from project 'Sample (net6.0-android)'
    Before:
            ICommand LoadingCommand(MaskType mask)
            {
                return new Command(async () =>
                {
                    var cancelSrc = new CancellationTokenSource();
                    var config = new ProgressDialogConfig()
                        .SetTitle("Loading")
                        .SetIsDeterministic(false)
                        .SetMaskType(mask)
                        .SetCancel(onCancel: cancelSrc.Cancel);
    After:
            ICommand LoadingCommand(MaskType mask) => new Command(async () =>
                                                       {
                                                           var cancelSrc = new CancellationTokenSource();
                                                           var config = new ProgressDialogConfig()
                                                               .SetTitle("Loading")
                                                               .SetIsDeterministic(false)
                                                               .SetMaskType(mask)
                                                               .SetCancel(onCancel: cancelSrc.Cancel);
    */


    private ICommand LoadingCommand(MaskType mask) => new Command(async () =>
                                                       {
                                                           var cancelSrc = new CancellationTokenSource();
                                                           var config = new ProgressDialogConfig()
                                                               .SetTitle("Loading")
                                                               .SetIsDeterministic(false)
                                                               .SetMaskType(mask)
                                                               .SetCancel(onCancel: cancelSrc.Cancel);


                                                           /* Unmerged change from project 'Sample (net6.0-ios)'
                                                           Before:
                                                                           using (this.Dialogs.Progress(config))
                                                                           {
                                                                               try
                                                                               {
                                                                                   await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                                                                               }
                                                                               catch { }
                                                                           }
                                                                           this.Result(cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
                                                                       });
                                                                   }
                                                               }
                                                           }
                                                           After:
                                                                                                                  using (this.Dialogs.Progress(config))
                                                                                                                      try
                                                                                                                      {
                                                                                                                          await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                                                                                                                      }
                                                                                                                      catch { }
                                                                                                                  Result(cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
                                                                                                              });
                                                           }
                                                           */

                                                           /* Unmerged change from project 'Sample (net6.0-android)'
                                                           Before:
                                                                           using (this.Dialogs.Progress(config))
                                                                           {
                                                                               try
                                                                               {
                                                                                   await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                                                                               }
                                                                               catch { }
                                                                           }
                                                                           this.Result(cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
                                                                       });
                                                                   }
                                                               }
                                                           }
                                                           After:
                                                                                                                  using (this.Dialogs.Progress(config))
                                                                                                                      try
                                                                                                                      {
                                                                                                                          await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                                                                                                                      }
                                                                                                                      catch { }
                                                                                                                  Result(cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
                                                                                                              });
                                                           }
                                                           */
                                                           using (Dialogs.Progress(config))
                                                               try
                                                               {
                                                                   await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                                                               }
                                                               catch { }
                                                           Result(cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
                                                       });
}
