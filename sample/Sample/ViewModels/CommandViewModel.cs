using System.Windows.Input;

namespace Sample.ViewModels;

public class CommandViewModel
{
    public string Text { get; set; }
    public ICommand Command { get; set; }
}
