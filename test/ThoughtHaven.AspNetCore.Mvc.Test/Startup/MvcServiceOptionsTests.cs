using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Startup
{
    public class MvcServiceOptionsTests
    {
        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsMvc()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Mvc);
                }

                [Fact]
                public void WhenCalled_SetsJson()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Json);
                }

                [Fact]
                public void WhenCalled_SetsRazor()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Razor);
                }

                [Fact]
                public void WhenCalled_SetsViews()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Views);
                }

                [Fact]
                public void WhenCalled_SetsAntiforgery()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Antiforgery);
                }

                [Fact]
                public void WhenCalled_SetsRouting()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Routing);
                }

                [Fact]
                public void WhenCalled_SetsCookiePolicy()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.CookiePolicy);
                }
            }
        }

        public class MvcProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new MvcOptions();

                    configure.Mvc(options);

                    Assert.NotNull(options.Filters.Single(
                        filter => filter is AutoValidateAntiforgeryTokenAttribute));
                    Assert.NotNull(options.Filters.Single(
                        filter => filter is RequireHttpsAttribute));
                    Assert.True(options.RequireHttpsPermanent);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Mvc = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<MvcOptions>(o => { });

                    configure.Mvc = options;

                    Assert.Equal(options, configure.Mvc);
                }
            }
        }

        public class JsonProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new MvcJsonOptions();

                    configure.Json(options);

                    Assert.Equal(Formatting.None, options.SerializerSettings.Formatting);
                    Assert.Equal(NullValueHandling.Ignore,
                        options.SerializerSettings.NullValueHandling);
                    Assert.True(options.SerializerSettings.ContractResolver is
                        CamelCasePropertyNamesContractResolver);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Json = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<MvcJsonOptions>(o => { });

                    configure.Json = options;

                    Assert.Equal(options, configure.Json);
                }
            }
        }

        public class RazorProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresAreaViewLocationFormats()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();

                    configure.Razor(options);

                    Assert.Equal(8, options.AreaViewLocationFormats.Count);
                    Assert.Equal("/Features/{2}/{1}/{0}.cshtml",
                        options.AreaViewLocationFormats[0]);
                    Assert.Equal("/Features/{2}/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[1]);
                    Assert.Equal("/Features/{2}/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats[2]);
                    Assert.Equal("/Features/{2}/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[3]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats[4]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[5]);
                    Assert.Equal("/Areas/{2}/Views/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[6]);
                    Assert.Equal("/Areas/{2}/Views/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[7]);
                }

                [Fact]
                public void ExistingAreaViewLocationFormats_MovesExistingAreaViewLocationsToAfterFeatures()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add(
                        "/Areas/{2}/Views/Shared/{0}.cshtml");

                    configure.Razor(options);

                    Assert.Equal(10, options.AreaViewLocationFormats.Count);
                    Assert.Equal("/Features/{2}/{1}/{0}.cshtml",
                        options.AreaViewLocationFormats[0]);
                    Assert.Equal("/Features/{2}/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[1]);
                    Assert.Equal("/Features/{2}/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats[2]);
                    Assert.Equal("/Features/{2}/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[3]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats[4]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[5]);
                    Assert.Equal("/Areas/{2}/Views/{1}/{0}.cshtml",
                        options.AreaViewLocationFormats[6]);
                    Assert.Equal("/Areas/{2}/Views/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats[7]);
                    Assert.Equal("/Areas/{2}/Views/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[8]);
                    Assert.Equal("/Areas/{2}/Views/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats[9]);
                }

                [Fact]
                public void DefaultValue_ConfiguresViewLocationFormats()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();

                    configure.Razor(options);

                    Assert.Equal(6, options.ViewLocationFormats.Count);
                    Assert.Equal("/Features/{1}/{0}.cshtml",
                        options.ViewLocationFormats[0]);
                    Assert.Equal("/Features/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats[1]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        options.ViewLocationFormats[2]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats[3]);
                    Assert.Equal("/Views/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats[4]);
                    Assert.Equal("/Views/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats[5]);
                }

                [Fact]
                public void ExistingViewLocationFormats_MovesExistingViewLocationsToAfterFeatures()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();
                    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

                    configure.Razor(options);

                    Assert.Equal(8, options.ViewLocationFormats.Count);
                    Assert.Equal("/Features/{1}/{0}.cshtml",
                        options.ViewLocationFormats[0]);
                    Assert.Equal("/Features/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats[1]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        options.ViewLocationFormats[2]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats[3]);
                    Assert.Equal("/Views/{1}/{0}.cshtml",
                        options.ViewLocationFormats[4]);
                    Assert.Equal("/Views/Shared/{0}.cshtml",
                        options.ViewLocationFormats[5]);
                    Assert.Equal("/Views/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats[6]);
                    Assert.Equal("/Views/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats[7]);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Razor = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<RazorViewEngineOptions>(o => { });

                    configure.Razor = options;

                    Assert.Equal(options, configure.Razor);
                }
            }
        }

        public class ViewProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new MvcViewOptions();

                    configure.Views(options);

                    Assert.False(options.HtmlHelperOptions.ClientValidationEnabled);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Views = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<MvcViewOptions>(o => { });

                    configure.Views = options;

                    Assert.Equal(options, configure.Views);
                }
            }
        }

        public class AntiforgeryProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new AntiforgeryOptions();

                    configure.Antiforgery(options);

                    Assert.Equal(".xsrf", options.FormFieldName);
                    Assert.Equal(".xsrf", options.HeaderName);
                    Assert.Equal(".xsrf", options.Cookie.Name);
                    Assert.True(options.Cookie.HttpOnly);
                    Assert.Equal(CookieSecurePolicy.Always, options.Cookie.SecurePolicy);
                    Assert.False(options.SuppressXFrameOptionsHeader);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Antiforgery = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<AntiforgeryOptions>(o => { });

                    configure.Antiforgery = options;

                    Assert.Equal(options, configure.Antiforgery);
                }
            }
        }

        public class RoutingProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RouteOptions();

                    configure.Routing(options);

                    Assert.False(options.AppendTrailingSlash);
                    Assert.True(options.LowercaseUrls);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Routing = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<RouteOptions>(o => { });

                    configure.Routing = options;

                    Assert.Equal(options, configure.Routing);
                }
            }
        }

        public class CookiePolicyProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = new CookiePolicyOptions();

                    configure.CookiePolicy(options);

                    Assert.True(options.CheckConsentNeeded(new DefaultHttpContext()));
                    Assert.Equal(SameSiteMode.None, options.MinimumSameSitePolicy);
                    Assert.Equal(CookieSecurePolicy.Always, options.Secure);
                    Assert.Equal(CookieSecurePolicy.Always,
                        options.ConsentCookie.SecurePolicy);
                    Assert.Equal(".gdpr.consent", options.ConsentCookie.Name);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().CookiePolicy = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<CookiePolicyOptions>(o => { });

                    configure.CookiePolicy = options;

                    Assert.Equal(options, configure.CookiePolicy);
                }
            }
        }
    }
}