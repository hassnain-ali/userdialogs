using System.Collections.Generic;
using Android.OS;


namespace Acr.UserDialogs.Fragments;

public class ConfigStore
{
    public string BundleKey { get; set; } = "UserDialogFragmentConfig";

    private long counter = 0;
    private readonly IDictionary<long, object> configStore = new Dictionary<long, object>();


    public static ConfigStore Instance { get; } = new ConfigStore();


    public void Store(Bundle bundle, object config)
    {
        counter++;
        configStore[counter] = config;
        bundle.PutLong(BundleKey, counter);
    }


    public bool Contains(Bundle bundle) => configStore.ContainsKey(bundle?.GetLong(BundleKey, -1) ?? -1);


    //public bool TryPop<T>(Bundle bundle, out T config) where T : class
    //{
    //    config = null;
    //    if (!this.Contains(bundle))
    //        return false;

    //    config = this.Pop<T>(bundle);
    //    return true;
    //}


    public T Pop<T>(Bundle bundle) where T : class
    {
        var id = bundle.GetLong(BundleKey);
        var cfg = (T)configStore[id];
        _ = configStore.Remove(id);
        return cfg;
    }
}
