using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Rugal.JavaScriptStore.Services;

namespace Rugal.JavaScriptStore.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public partial class JsStoreAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var HttpContext = context.HttpContext;
        var Provider = HttpContext.RequestServices;
        var JsStore = Provider.GetService<JsStoreService>();
        JsStore.Setting.ActionExecuted(Provider, JsStore);
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var HttpContext = context.HttpContext;
        var Provider = HttpContext.RequestServices;
        var JsStore = Provider.GetService<JsStoreService>();
        var Query = HttpContext.Request.Query;
        foreach (var Item in Query)
        {
            var Value = Item.Value.ToString();
            if (string.IsNullOrWhiteSpace(Value))
                Value = null;

            JsStore.AddQuery(Item.Key, Value);
        }
        JsStore.Setting.ActionExecuting(Provider, JsStore);
    }
}