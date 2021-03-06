﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text.Json;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
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
                public void WhenCalled_SetsRouting()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.Routing);
                }

                [Fact]
                public void WhenCalled_SetsTrackingConsent()
                {
                    var options = new MvcServiceOptions();

                    Assert.NotNull(options.TrackingConsent);
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
                        new MvcServiceOptions().Mvc = null!;
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
                    var options = new JsonOptions();

                    configure.Json(options);

                    Assert.False(options.JsonSerializerOptions.WriteIndented);
                    Assert.True(options.JsonSerializerOptions.IgnoreNullValues);
                    Assert.Equal(JsonNamingPolicy.CamelCase,
                        options.JsonSerializerOptions.PropertyNamingPolicy);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Json = null!;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<JsonOptions>(o => { });

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
                public void DefaultValue_AddsPartialFolderViewLocations()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();
                    options.AreaViewLocationFormats.Clear();
                    options.ViewLocationFormats.Clear();

                    configure.Razor(options);

                    Assert.Contains("/Areas/{2}/Views/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Areas/{2}/Views/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);

                    Assert.Contains("/Views/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/Views/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                }

                [Fact]
                public void DefaultValue_AddsRootFolderViewLocations()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();
                    options.AreaViewLocationFormats.Clear();
                    options.ViewLocationFormats.Clear();

                    configure.Razor(options);

                    Assert.Contains("/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/{2}/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/{2}/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/{2}/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/{2}/{1}/{0}.cshtml",
                        options.AreaViewLocationFormats);

                    Assert.Contains("/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/Shared/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/{1}/{0}.cshtml",
                        options.ViewLocationFormats);
                }

                [Fact]
                public void DefaultValue_AddsFeatureFolderViewLocations()
                {
                    var configure = new MvcServiceOptions();
                    var options = new RazorViewEngineOptions();
                    options.AreaViewLocationFormats.Clear();
                    options.ViewLocationFormats.Clear();

                    configure.Razor(options);

                    Assert.Contains("/Features/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Features/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Features/{2}/Shared/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Features/{2}/Shared/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Features/{2}/{1}/Partials/{0}.cshtml",
                        options.AreaViewLocationFormats);
                    Assert.Contains("/Features/{2}/{1}/{0}.cshtml",
                        options.AreaViewLocationFormats);

                    Assert.Contains("/Features/Shared/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/Features/Shared/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/Features/{1}/Partials/{0}.cshtml",
                        options.ViewLocationFormats);
                    Assert.Contains("/Features/{1}/{0}.cshtml",
                        options.ViewLocationFormats);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Razor = null!;
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
                        new MvcServiceOptions().Views = null!;
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
                    Assert.False(options.LowercaseQueryStrings);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().Routing = null!;
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

        public class StaticFilesProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ConfiguresOptions()
                {
                    var configure = new MvcServiceOptions();
                    var options = Options();
                    var responseContext = ResponseContext();

                    configure.StaticFiles(options);

                    options.OnPrepareResponse(responseContext);

                    Assert.Equal("public,max-age=31536000",
                        responseContext.Context.Response.Headers[HeaderNames.CacheControl]);
                }

                [Fact]
                public void DefaultValue_PreservesExistingAction()
                {
                    var configure = new MvcServiceOptions();
                    var onPrepareResponseCalled = false;
                    var options = Options();
                    options.OnPrepareResponse = context =>
                    {
                        onPrepareResponseCalled = true;
                    };
                    var responseContext = ResponseContext();

                    configure.StaticFiles(options);

                    options.OnPrepareResponse(responseContext);

                    Assert.True(onPrepareResponseCalled);
                    Assert.Equal("public,max-age=31536000",
                        responseContext.Context.Response.Headers[HeaderNames.CacheControl]);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().StaticFiles = null!;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new Action<StaticFileOptions>(o => { });

                    configure.StaticFiles = options;

                    Assert.Equal(options, configure.StaticFiles);
                }
            }

            private static StaticFileOptions Options() => new StaticFileOptions();
            private static StaticFileResponseContext ResponseContext()
            {
                var context = new StaticFileResponseContext(new DefaultHttpContext(),
                    file: new FakeFileInfo());

                //var type = context.GetType();
                //var property = type.GetProperty(nameof(context.Context));
                //property!.SetValue(context, new DefaultHttpContext(), null);

                return context;
            }
        }

        public class TrackingConsentProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ReturnsOptions()
                {
                    var configure = new MvcServiceOptions();

                    Assert.NotNull(configure.TrackingConsent);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new MvcServiceOptions().TrackingConsent = null!;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var configure = new MvcServiceOptions();
                    var options = new TrackingConsentOptions();

                    configure.TrackingConsent = options;

                    Assert.Equal(options, configure.TrackingConsent);
                }
            }
        }
    }
}