using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using ThoughtHaven.AspNetCore.Tracking;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TrackingConsentServiceCollectionExtensionsTests
    {
        public class AddTrackingConsentMethod
        {
            public class ServicesAndOptionsOverload
            {
                [Fact]
                public void NullServices_Throws()
                {
                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        TrackingConsentServiceCollectionExtensions.AddTrackingConsent(
                            services: null);
                    });
                }

                [Fact]
                public void WhenCalled_AddsAntiforgery()
                {
                    var services = Services();

                    services.AddTrackingConsent();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

                    Assert.Equal(".xsrf", options.FormFieldName);
                    Assert.Equal(".xsrf", options.HeaderName);
                    Assert.Equal(".xsrf", options.Cookie.Name);
                    Assert.True(options.Cookie.HttpOnly);
                    Assert.Equal(CookieSecurePolicy.Always, options.Cookie.SecurePolicy);
                    Assert.False(options.SuppressXFrameOptionsHeader);
                }

                [Fact]
                public void OptionsHasAntiforgery_ConfirguresAntiforgery()
                {
                    var services = Services();

                    services.AddTrackingConsent(new TrackingConsentOptions()
                    {
                        Antiforgery = o => o.Cookie.Name = "Custom"
                    });

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

                    Assert.Equal("Custom", options.Cookie.Name);
                }

                [Fact]
                public void WhenCalled_AddsCookiePolicy()
                {
                    var services = Services();

                    services.AddTrackingConsent();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<CookiePolicyOptions>>().Value;

                    Assert.True(options.CheckConsentNeeded(null));
                    Assert.Equal(SameSiteMode.Lax, options.MinimumSameSitePolicy);
                    Assert.Equal(HttpOnlyPolicy.Always, options.HttpOnly);
                    Assert.Equal(CookieSecurePolicy.Always, options.Secure);
                    Assert.Equal(".tracking.consent", options.ConsentCookie.Name);
                    Assert.True(options.ConsentCookie.HttpOnly);
                    Assert.Equal(CookieSecurePolicy.Always,
                        options.ConsentCookie.SecurePolicy);
                }

                [Fact]
                public void OptionsHasCookiePolicy_ConfirguresCookiePolicy()
                {
                    var services = Services();

                    services.AddTrackingConsent(new TrackingConsentOptions()
                    {
                        CookiePolicy = o => o.ConsentCookie.Name = "Custom"
                    });

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<CookiePolicyOptions>>().Value;

                    Assert.Equal("Custom", options.ConsentCookie.Name);
                }

                [Fact]
                public void WhenCalled_AddsIHttpContextAccessor()
                {
                    var services = Services();

                    services.AddTrackingConsent();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<IHttpContextAccessor>();

                    Assert.NotNull(service);
                    Assert.IsType<HttpContextAccessor>(service);
                }

                [Fact]
                public void WhenCalled_AddsITrackingConsentService()
                {
                    var services = Services();

                    services.AddTrackingConsent();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<ITrackingConsentService>();

                    Assert.NotNull(service);
                    Assert.IsType<TrackingConsentService>(service);
                }

                [Fact]
                public void WhenCalled_ReturnsServices()
                {
                    var services = Services();

                    var result = services.AddTrackingConsent();

                    Assert.Equal(result, services);
                }
            }
        }

        private static ServiceCollection Services() => new ServiceCollection();
    }
}