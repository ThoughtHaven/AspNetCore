using ThoughtHaven;
using ThoughtHaven.AspNetCore.SecurityHeaders;

namespace Microsoft.AspNetCore.Builder
{
    public static class SecurityHeaderBuilderExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app,
            SecurityHeaderOptions? options = null)
        {
            Guard.Null(nameof(app), app);

            app.UseHsts();

            return app.UseMiddleware<SecurityHeaderMiddleware>(
                options ?? new SecurityHeaderOptions());
        }
    }
}