using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ThoughtHaven.AspNetCore.Mvc.UI
{
    public static class MvcUIBuilderExtensions
    {
        public static IApplicationBuilder UseUI(this IApplicationBuilder app,
            UIStaticFileOptions options)
        {
            if (app == null) { throw new ArgumentNullException(nameof(app)); }
            if (options == null) { throw new ArgumentNullException(nameof(options)); }

            var service = app.ApplicationServices.GetService<MvcMarkerService>();
            if (service == null)
            {
                throw new InvalidOperationException($"Thought Haven Mvc UI is built on top of ASP.NET Core Mvc. Please add all the required services by calling '{nameof(IServiceCollection)}.{nameof(MvcServiceCollectionExtensions.AddMvc)}' inside the call to '{nameof(IStartup.ConfigureServices)}(...)' in the application startup code.");
            }

            app.UseStaticFiles(options);

            return app;
        }
    }
}