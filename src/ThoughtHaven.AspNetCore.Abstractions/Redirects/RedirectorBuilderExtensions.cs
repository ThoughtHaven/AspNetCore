using Microsoft.AspNetCore.Routing;
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
                    var routeData = context.GetRouteData();

                    var location = BuildLocation(redirect, routeData);

                    context.Response.Redirect(location, redirect.Permanent);

                    return Task.CompletedTask;
                });
            }

            return app.UseRouter(routes.Build());
        }

        public static string BuildLocation(RedirectRoute redirect, RouteData routeData)
        {
            Guard.Null(nameof(redirect), redirect);
            Guard.Null(nameof(routeData), routeData);

            var location = redirect.Location;

            foreach (var kvp in routeData.Values)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                location = location.Replace($"{{{key}}}", value!.ToString());
            }

            return location;
        }
    }
}