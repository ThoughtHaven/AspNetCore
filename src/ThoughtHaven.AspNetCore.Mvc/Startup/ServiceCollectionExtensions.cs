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
            MvcConfigureOptions configure = null)
        {
            Guard.Null(nameof(services), services);

            configure = configure ?? new MvcConfigureOptions();

            services.TryAddSingleton<SystemClock>();

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure(configure.CookiePolicy);

            var mvc = services.AddMvc(configure.Mvc)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(configure.Json)
                .AddRazorOptions(configure.Razor)
                .AddViewOptions(configure.Views);

            services.AddAntiforgery(configure.Antiforgery)
                .AddRouting(configure.Routing);

            return mvc;
        }
    }
}