﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using ThoughtHaven.AspNetCore.Tracking;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Startup
{
    public class ThoughtHavenMvcServiceCollectionExtensionsTests
    {
        public class AddThoughtHavenMvcMethod
        {
            public class ServicesAndConfigureOverload
            {
                [Fact]
                public void NullServices_Throws()
                {
                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        ((IServiceCollection)null!).AddThoughtHavenMvc();
                    });
                }

                [Fact]
                public void WhenCalled_AddsSystemClock()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    Assert.NotNull(services.BuildServiceProvider()
                        .GetRequiredService<SystemClock>());
                }

                [Fact]
                public void WhenCalled_AddsActionContextAccessor()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<IActionContextAccessor>();

                    Assert.NotNull(service);
                    Assert.True(service is ActionContextAccessor);
                }

                [Fact]
                public void WhenCalled_AddsHttpContextAccessor()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<IHttpContextAccessor>();

                    Assert.NotNull(service);
                    Assert.True(service is HttpContextAccessor);
                }

                [Fact]
                public void WhenCalled_AddsTrackingConsentService()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<ITrackingConsentService>();

                    Assert.NotNull(service);
                    Assert.IsType<TrackingConsentService>(service);
                }

                [Fact]
                public void WhenCalled_CallsPostConfigureStaticFiles()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var service = services.BuildServiceProvider()
                        .GetRequiredService<IPostConfigureOptions<StaticFileOptions>>();

                    Assert.NotNull(service);
                }

                [Fact]
                public void WhenCalled_AddsMvcOptions()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcOptions>>().Value;

                    Assert.NotNull(options);
                }

                [Fact]
                public void WhenCalled_ConfiguresMvcOptions()
                {
                    var services = Services();
                    var configure = new FakeMvcServiceOptions();

                    services.AddThoughtHavenMvc(configure);

                    _ = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcOptions>>().Value;

                    Assert.True(configure.Mvc_Called);
                }

                [Fact]
                public void WhenCalled_AddsMvcJsonOptions()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<JsonOptions>>().Value;

                    Assert.NotNull(options);
                }

                [Fact]
                public void WhenCalled_ConfiguresMvcJsonOptions()
                {
                    var services = Services();
                    var configure = new FakeMvcServiceOptions();

                    services.AddThoughtHavenMvc(configure);

                    _ = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcOptions>>().Value;

                    Assert.True(configure.Json_Called);
                }

                [Fact]
                public void WhenCalled_AddsRazorViewEngineOptions()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RazorViewEngineOptions>>().Value;

                    Assert.NotNull(options);
                }

                [Fact]
                public void WhenCalled_ConfiguresRazorViewEngineOptionsOptions()
                {
                    var services = Services();
                    var configure = new FakeMvcServiceOptions();

                    services.AddThoughtHavenMvc(configure);

                    _ = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RazorViewEngineOptions>>().Value;

                    Assert.True(configure.Razor_Called);
                }

                [Fact]
                public void WhenCalled_AddsMvcViewOptions()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcViewOptions>>().Value;

                    Assert.NotNull(options);
                }

                [Fact]
                public void WhenCalled_ConfiguresMvcViewOptions()
                {
                    var services = Services();
                    var configure = new FakeMvcServiceOptions();

                    services.AddThoughtHavenMvc(configure);

                    _ = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcViewOptions>>().Value;

                    Assert.True(configure.Views_Called);
                }

                [Fact]
                public void WhenCalled_AddsRouteOptions()
                {
                    var services = Services();

                    services.AddThoughtHavenMvc();

                    var options = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RouteOptions>>().Value;

                    Assert.NotNull(options);
                }

                [Fact]
                public void WhenCalled_ConfiguresRouteOptions()
                {
                    var services = Services();
                    var configure = new FakeMvcServiceOptions();

                    services.AddThoughtHavenMvc(configure);

                    _ = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RouteOptions>>().Value;

                    Assert.True(configure.Routing_Called);
                }

                [Fact]
                public void WhenCalled_ReturnsMvcBuilder()
                {
                    var services = Services();

                    var mvc = services.AddThoughtHavenMvc();

                    Assert.NotNull(mvc);
                }
            }
        }

        public static FakeServiceCollection Services() => new FakeServiceCollection();
    }
}