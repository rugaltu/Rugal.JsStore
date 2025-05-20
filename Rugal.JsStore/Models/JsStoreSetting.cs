using Rugal.JavaScriptStore.Services;

namespace Rugal.JavaScriptStore.Models;
public class JsStoreSetting
{
    public Dictionary<string, Dictionary<string, object>> DefaultStore { get; private set; }
    public event Action<IServiceProvider, JsStoreService> OnActionExecuted;
    public event Action<IServiceProvider, JsStoreService> OnActionExecuting;
    public JsStoreSetting()
    {
        DefaultStore = [];
    }
    public JsStoreSetting AddDefaultStore(string StoreKey, string Key, object Value)
    {
        if (!DefaultStore.TryGetValue(StoreKey, out var Store))
        {
            Store = [];
            DefaultStore.Add(StoreKey, Store);
        }
        if (!Store.TryAdd(Key, Value))
            Store[Key] = Value;

        return this;
    }
    public JsStoreSetting AddDefaultStore(string Key, object Value)
    {
        AddDefaultStore(JsStoreService.DefaultStoreKey, Key, Value);
        return this;
    }
    public void ActionExecuted(IServiceProvider Provider, JsStoreService JsStore)
    {
        OnActionExecuted?.Invoke(Provider, JsStore);
    }
    public void ActionExecuting(IServiceProvider Provider, JsStoreService JsStore)
    {
        OnActionExecuting?.Invoke(Provider, JsStore);
    }
}
