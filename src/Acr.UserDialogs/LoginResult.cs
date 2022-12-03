namespace Acr.UserDialogs;


public class LoginResult : AbstractStandardDialogResult<Credentials>
{
    public string LoginText => Value.UserName;
    public string Password => Value.Password;


    public LoginResult(bool ok, string login, string pass) : base(ok, new Credentials(login, pass))
    {
    }
}
