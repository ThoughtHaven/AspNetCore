using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThoughtHaven;
using ThoughtHaven.AspNetCore.Redirects;

namespace Microsoft.AspNetCore.Builder
{
    public static class RedirectorBuilderExtensions
    {
        public static IApplicationBuilder UseRedirector(this IApplicationBuilder app,
            RedirectRoute[] redirects)
        {
            Guard.Null(nameof(app), app);
            Guard.Null(nameof(redirects), redirects);
            Guard.NullItem(nameof(redirects), redirects);

            var routes = new RouteBuilder(app);

            foreach (var redirect in redirects)
            {
                routes.MapGet(redirect.Template, context =>
                {
                    var location = redirect.Location;
                    var routeData = context.GetRouteData();

                    foreach (var kvp in routeData.Values)
                    {
                        location = location.Replace($"{{{kvp.Key}}}", kvp.Value.ToString());
                    }

                    context.Response.Redirect(location, redirect.Permanent);

                    return Task.CompletedTask;
                });
            }

            return app.UseRouter(routes.Build());
        }
    }
}