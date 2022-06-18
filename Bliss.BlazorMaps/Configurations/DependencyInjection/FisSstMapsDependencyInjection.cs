using Bliss.BlazorMaps.JsInterops.Events;
using Bliss.BlazorMaps.JsInterops.IconFactories;
using Bliss.BlazorMaps.JsInterops.Maps;
using Microsoft.Extensions.DependencyInjection;

namespace Bliss.BlazorMaps.DependencyInjection
{
    /// <summary>
    /// It is responsible for providing an app's services
    /// collection with its Factories and JsInterops implementations.
    /// </summary>
    public static class BlissMapsDependencyInjection
    {
        public static IServiceCollection AddBlazorLeafletMaps(this IServiceCollection services)
        {
            AddJsInterops(services);
            AddFactories(services);
            return services;
        }

        private static void AddFactories(IServiceCollection services)
        {
            services.AddScoped<IMarkerFactory, MarkerFactory>();
            services.AddScoped<IIconFactory, IconFactory>();
            services.AddScoped<IPolylineFactory, PolylineFactory>();
            services.AddScoped<IPolygonFactory, PolygonFactory>();
            services.AddScoped<ICircleMarkerFactory, CircleMarkerFactory>();
            services.AddScoped<ICircleFactory, CircleFactory>();
        }

        private static void AddJsInterops(IServiceCollection services)
        {
            services.AddScoped<IMapJsInterop, MapJsInterop>();
            services.AddScoped<IEventedJsInterop, EventedJsInterop>();
            services.AddScoped<IIconFactoryJsInterop, IconFactoryJsInterop>();
        }
    }
}
