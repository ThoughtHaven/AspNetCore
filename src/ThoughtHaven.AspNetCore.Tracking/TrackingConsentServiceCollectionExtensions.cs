using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using ThoughtHaven.AspNetCore.Tracking;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TrackingConsentServiceCollectionExtensions
    {
        public static IServiceCollection AddTrackingConsent(this IServiceCollection services,
            TrackingConsentOptions options = null)
        {
            if (services == null) { throw new ArgumentNullException(nameof(services)); }

            options = options ?? new TrackingConsentOptions();

            services.AddAntiforgery(antiforgery =>
            {
                string xsrf = ".xsrf";

                antiforgery.FormFieldName = xsrf;
                antiforgery.HeaderName = xsrf;
                antiforgery.Cookie.Name = xsrf;
                antiforgery.Cookie.HttpOnly = true;
                antiforgery.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                antiforgery.SuppressXFrameOptionsHeader = false;

                options.Antiforgery?.Invoke(antiforgery);
            });

            services.Configure<CookiePolicyOptions>(cookiePolicy =>
            {
                cookiePolicy.CheckConsentNeeded = context => true;
                cookiePolicy.MinimumSameSitePolicy = SameSiteMode.Lax;
                cookiePolicy.HttpOnly = HttpOnlyPolicy.Always;
                cookiePolicy.Secure = CookieSecurePolicy.Always;
                cookiePolicy.ConsentCookie.Name = ".tracking.consent";
                cookiePolicy.ConsentCookie.HttpOnly = true;
                cookiePolicy.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;

                options.CookiePolicy?.Invoke(cookiePolicy);
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddTransient<ITrackingConsentService, TrackingConsentService>();

            return services;
        }
    }
}