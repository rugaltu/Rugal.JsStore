using Rugal.JavaScriptStore.Service;

namespace Rugal.JavaScriptStore.Model
{
    public class JsStoreSetting
    {
        public Dictionary<string, Dictionary<string, object>> DefaultStore { get; private set; }
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
            AddDefaultStore(JsStoreService.JsStoreKey, Key, Value);
            return this;
        }
    }
}
