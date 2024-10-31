using Microsoft.Extensions.DependencyInjection;
using Rugal.JavaScriptStore.Model;
using Rugal.JavaScriptStore.Service;

namespace Rugal.JavaScriptStore.Extention;

public static class StartupExtention
{
    public static IServiceCollection AddJsStore(this IServiceCollection Services, Action<JsStoreSetting> SettingFunc = null)
    {
        var Setting = new JsStoreSetting();
        SettingFunc?.Invoke(Setting);

        Services.AddSingleton(Setting)
            .AddScoped<JsStoreService>();
        return Services;
    }

}
