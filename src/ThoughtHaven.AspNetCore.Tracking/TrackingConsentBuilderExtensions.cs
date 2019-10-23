namespace Microsoft.AspNetCore.Builder
{
    public static class TrackingConsentBuilderExtensions
    {
        public static IApplicationBuilder UseTrackingConsent(this IApplicationBuilder app)
        {
            if (app == null) { throw new System.ArgumentNullException(nameof(app)); }

            app.UseCookiePolicy();

            return app;
        }
    }
}