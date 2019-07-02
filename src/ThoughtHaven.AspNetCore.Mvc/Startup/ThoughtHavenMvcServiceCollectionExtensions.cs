using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ThoughtHaven;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThoughtHavenMvcServiceCollectionExtensions
    {
        public static IMvcBuilder AddThoughtHavenMvc(this IServiceCollection services,
            MvcServiceOptions? options = null)
        {
            Guard.Null(nameof(services), services);

            options ??= new MvcServiceOptions();

            services.TryAddSingleton<SystemClock>();

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTrackingConsent(options.TrackingConsent);

            services.PostConfigure(options.StaticFiles);

            var mvc = services.AddMvc(options.Mvc)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options.Json)
                .AddRazorOptions(options.Razor)
                .AddViewOptions(options.Views);

            services.AddRouting(options.Routing);

            return mvc;
        }
    }
}