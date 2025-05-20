using Microsoft.AspNetCore.Html;
using Rugal.JavaScriptStore.Models;
using System.Text.Json;

namespace Rugal.JavaScriptStore.Services;

public class JsStoreService
{
    public const string DefaultStoreKey = "Default";
    public const string QueryKey = "Query";
    public readonly JsStoreSetting Setting;
    public Dictionary<string, Dictionary<string, object>> JsStore { get; private set; }
    public JsStoreService(JsStoreSetting Setting)
    {
        this.Setting = Setting;
        JsStore = [];
        WithDefaultStore(DefaultStoreKey);
        WithDefaultStore(QueryKey);
    }
    public IHtmlContent RenderJs()
    {
        var ScriptString = $"window.jsStore = {JsonSerializer.Serialize(JsStore)}";
        var Result = new HtmlString(ScriptString);
        return Result;
    }
    public JsStoreService AddStore(string StoreKey, string Key, object Value)
    {
        BaseAdd(StoreKey, Key, Value);
        return this;
    }
    public JsStoreService AddStore(string Key, object Value)
    {
        BaseAdd(DefaultStoreKey, Key, Value);
        return this;
    }
    public JsStoreService RemoveStore(string StoreKey, string Key)
    {
        BaseRemove(StoreKey, Key);
        return this;
    }
    public JsStoreService RemoveStore(string Key)
    {
        BaseRemove(DefaultStoreKey, Key);
        return this;
    }
    public JsStoreService AddQuery(string Key, object Value)
    {
        BaseAdd(QueryKey, Key, Value);
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