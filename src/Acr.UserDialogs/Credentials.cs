﻿namespace Acr.UserDialogs;

public class Credentials
{
    public Credentials(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }


    public string UserName { get; }
    public string Password { get; }
}
