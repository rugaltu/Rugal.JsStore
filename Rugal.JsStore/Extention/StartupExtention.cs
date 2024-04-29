using Microsoft.Extensions.DependencyInjection;
using Rugal.JavaScriptStore.Model;
using Rugal.JavaScriptStore.Service;

namespace Rugal.JavaScriptStore.Extention
{
    public static class StartupExtention
    {
        public static IServiceCollection AddJsDI(this IServiceCollection Services)
        {
            Services.AddScoped<JsStoreService>();
            Services.AddScoped<JsStoreSetting>();
            return Services;
        }
    }
}
