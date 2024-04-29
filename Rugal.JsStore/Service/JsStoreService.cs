using Microsoft.AspNetCore.Html;
using Rugal.JavaScriptStore.Model;
using System.Text.Json;

namespace Rugal.JavaScriptStore.Service
{
    public class JsStoreService
    {
        public const string JsStoreKey = "JsStore";
        public const string QueryKey = "Query";
        private readonly JsStoreSetting Setting;
        public Dictionary<string, Dictionary<string, object>> JsStore { get; private set; }
        public JsStoreService(JsStoreSetting _Setting)
        {
            Setting = _Setting;
            JsStore = [];
            WithDefaultStore(JsStoreKey);
            WithDefaultStore(QueryKey);
        }
        public IHtmlContent RenderJs()
        {
            var JsVars = new List<string> { };
            foreach (var Store in JsStore)
            {
                var Js = $"const {Store.Key} = {JsonSerializer.Serialize(Store.Value)};";
                JsVars.Add(Js);
            }
            var JsResult = string.Join('\n', JsVars);
            var Result = new HtmlString(JsResult);
            return Result;
        }
        public JsStoreService AddStore(string StoreKey, string Key, object Value)
        {
            BaseAdd(StoreKey, Key, Value);
            return this;
        }
        public JsStoreService AddStore(string Key, object Value)
        {
            BaseAdd("JsStore", Key, Value);
            return this;
        }
        public JsStoreService RemoveStore(string StoreKey, string Key)
        {
            BaseRemove(StoreKey, Key);
            return this;
        }
        public JsStoreService RemoveStore(string Key)
        {
            BaseRemove(JsStoreKey, Key);
            return this;
        }
        public JsStoreService AddQuery(string Key, object Value)
        {
            BaseAdd("Query", Key, Value);
            return this;
        }
        public JsStoreService RemoveQuery(string Key)
        {
            BaseRemove(QueryKey, Key);
            return this;
        }
        public JsStoreService WithDefaultStore(string StoreKey)
        {
            JsStore.Add(StoreKey, []);
            if (Setting?.DefaultStore is not null)
            {
                foreach (var Store in Setting.DefaultStore)
                    foreach (var Item in Store.Value)
                        BaseAdd(Store.Key, Item.Key, Item.Value);
            }
            return this;
        }
        private void BaseAdd(string StoreKey, string Key, object Value)
        {
            if (!JsStore.TryGetValue(StoreKey, out var Store))
            {
                Store = [];
                JsStore.Add(StoreKey, Store);
            }
            if (!Store.TryAdd(Key, Value))
                Store[Key] = Value;
        }
        private void BaseRemove(string StoreKey, string Key)
        {
            if (!JsStore.TryGetValue(StoreKey, out var Store))
                return;

            Store.Remove(Key);
        }
    }
}