using Microsoft.AspNetCore.Antiforgery;
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
    public class MvcConfigureOptionsTests
    {
        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsMvc()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Mvc);
                }

                [Fact]
                public void WhenCalled_SetsJson()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Json);
                }

                [Fact]
                public void WhenCalled_SetsRazor()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Razor);
                }

                [Fact]
                public void WhenCalled_SetsViews()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Views);
                }

                [Fact]
                public void WhenCalled_SetsAntiforgery()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Antiforgery);
                }

                [Fact]
                public void WhenCalled_SetsRouting()
                {
                    var options = new MvcConfigureOptions();

                    Assert.NotNull(options.Routing);
                }
            }
        }

        public class MvcProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new MvcOptions();

                configure.Mvc(options);

                Assert.NotNull(options.Filters.Single(
                    filter => filter is AutoValidateAntiforgeryTokenAttribute));
                Assert.NotNull(options.Filters.Single(
                    filter => filter is RequireHttpsAttribute));
                Assert.True(options.RequireHttpsPermanent);
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Mvc = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<MvcOptions>(o => { });

                configure.Mvc = options;

                Assert.Equal(options, configure.Mvc);
            }
        }

        public class JsonProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new MvcJsonOptions();

                configure.Json(options);

                Assert.Equal(Formatting.None, options.SerializerSettings.Formatting);
                Assert.Equal(NullValueHandling.Ignore,
                    options.SerializerSettings.NullValueHandling);
                Assert.True(options.SerializerSettings.ContractResolver is
                    CamelCasePropertyNamesContractResolver);
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Json = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<MvcJsonOptions>(o => { });

                configure.Json = options;

                Assert.Equal(options, configure.Json);
            }
        }

        public class RazorProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new RazorViewEngineOptions();

                configure.Razor(options);

                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Areas/{2}/Views/{1}/Partials/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Areas/{2}/Views/Shared/Partials/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/{2}/{1}/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/{2}/{1}/Partials/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/{2}/Shared/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/{2}/Shared/Partials/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/Shared/{0}.cshtml"));
                Assert.True(options.AreaViewLocationFormats.Contains(
                    "/Features/Shared/Partials/{0}.cshtml"));

                Assert.True(options.ViewLocationFormats.Contains(
                    "/Views/{1}/Partials/{0}.cshtml"));
                Assert.True(options.ViewLocationFormats.Contains(
                    "/Views/Shared/Partials/{0}.cshtml"));

                Assert.True(options.ViewLocationFormats.Contains(
                    "/Features/{1}/{0}.cshtml"));
                Assert.True(options.ViewLocationFormats.Contains(
                    "/Features/{1}/Partials/{0}.cshtml"));
                Assert.True(options.ViewLocationFormats.Contains(
                    "/Features/Shared/{0}.cshtml"));
                Assert.True(options.ViewLocationFormats.Contains(
                    "/Features/Shared/Partials/{0}.cshtml"));
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Razor = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<RazorViewEngineOptions>(o => { });

                configure.Razor = options;

                Assert.Equal(options, configure.Razor);
            }
        }

        public class ViewProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new MvcViewOptions();

                configure.Views(options);

                Assert.False(options.HtmlHelperOptions.ClientValidationEnabled);
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Views = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<MvcViewOptions>(o => { });

                configure.Views = options;

                Assert.Equal(options, configure.Views);
            }
        }

        public class AntiforgeryProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new AntiforgeryOptions();

                configure.Antiforgery(options);

                Assert.Equal(".xsrf", options.FormFieldName);
                Assert.Equal(".xsrf", options.HeaderName);
                Assert.Equal(".xsrf", options.Cookie.Name);
                Assert.True(options.Cookie.HttpOnly);
                Assert.Equal(CookieSecurePolicy.Always, options.Cookie.SecurePolicy);
                Assert.False(options.SuppressXFrameOptionsHeader);
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Antiforgery = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<AntiforgeryOptions>(o => { });

                configure.Antiforgery = options;

                Assert.Equal(options, configure.Antiforgery);
            }
        }

        public class RoutingProperty
        {
            [Fact]
            public void Get_DefaultValue_ConfiguresOptions()
            {
                var configure = new MvcConfigureOptions();
                var options = new RouteOptions();

                configure.Routing(options);

                Assert.False(options.AppendTrailingSlash);
                Assert.True(options.LowercaseUrls);
            }

            [Fact]
            public void Set_ToNull_Throws()
            {
                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MvcConfigureOptions().Routing = null;
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var configure = new MvcConfigureOptions();
                var options = new Action<RouteOptions>(o => { });

                configure.Routing = options;

                Assert.Equal(options, configure.Routing);
            }
        }
    }
}