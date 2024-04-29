using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Rugal.JavaScriptStore.Service;

namespace Rugal.JavaScriptStore.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public partial class JsDIAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var HttpContext = context.HttpContext;
            var Query = HttpContext.Request.Query;
            var JsDIService = HttpContext.RequestServices.GetService<JsStoreService>();
            foreach (var Item in Query)
            {
                var Value = Item.Value.ToString();
                if (string.IsNullOrWhiteSpace(Value))
                    Value = null;

                JsDIService.AddQuery(Item.Key, Value);
            }
        }
    }
}