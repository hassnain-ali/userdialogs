using System;


namespace Acr.UserDialogs;

public static partial class UserDialogs
{
#if NETSTANDARD

    /* Unmerged change from project 'Acr.UserDialogs (netstandard2.0)'
    Before:
            static IUserDialogs currentInstance;
    After:
        private static IUserDialogs currentInstance;
    */
    private static IUserDialogs currentInstance;
    public static IUserDialogs Instance
    {
        get
        {

            /* Unmerged change from project 'Acr.UserDialogs (netstandard2.0)'
            Before:
                            if (currentInstance == null)
                                throw new ArgumentException("[Acr.UserDialogs] This is the bait library, not the platform library.  You must install the nuget package in your main executable/application project");

                            return currentInstance;
            After:
                        return (currentInstance == null)
                            ? throw new ArgumentException("[Acr.UserDialogs] This is the bait library, not the platform library.  You must install the nuget package in your main executable/application project")
                            : currentInstance;
            */
            return currentInstance ?? throw new ArgumentException("[Acr.UserDialogs] This is the bait library, not the platform library.  You must install the nuget package in your main executable/application project");
        }
        set => currentInstance = value;
    }
#endif
}
