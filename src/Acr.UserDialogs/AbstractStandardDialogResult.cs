namespace Acr.UserDialogs;

public abstract class AbstractStandardDialogResult<T> : IStandardDialogResult<T>
{

    protected AbstractStandardDialogResult(bool ok, T value)
    {
        Ok = ok;
        Value = value;
    }


    public bool Ok { get; }
    public T Value { get; }
}
