using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThoughtHaven.AspNetCore.Redirects;
using Xunit;

namespace Microsoft.AspNetCore.Builder
{
    public class RedirectorBuilderExtensionsTests
    {
        public class UseRedirectorMethod
        {
            public class AppAndRedirectsOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        RedirectorBuilderExtensions.UseRedirector(
                            app: null!,
                            redirects: Redirects());
                    });
                }

                [Fact]
                public void NullRedirects_Throws()
                {
                    Assert.Throws<ArgumentNullException>("redirects", () =>
                    {
                        RedirectorBuilderExtensions.UseRedirector(
                            app: App(),
                            redirects: null!);
                    });
                }

                [Fact]
                public void NullItemInRedirects_Throws()
                {
                    Assert.Throws<ArgumentException>("redirects", () =>
                    {
                        RedirectorBuilderExtensions.UseRedirector(
                            app: App(),
                            redirects: new RedirectRoute[] { null! });
                    });
                }

                [Fact]
                public void WhenCalled_ReturnsBuilder()
                {
                    var app = App();

                    var result = app.UseRedirector(Redirects());

                    Assert.Equal(app, result);
                }
            }
        }

        public class BuildLocationMethod
        {
            public class RedirectAndRouteDataOverload
            {
                [Fact]
                public void NullRedirect_Throws()
                {
                    Assert.Throws<ArgumentNullException>("redirect", () =>
                    {
                        RedirectorBuilderExtensions.BuildLocation(
                            redirect: null!,
                            routeData: RouteData());
                    });
                }

                [Fact]
                public void NullRouteData_Throws()
                {
                    Assert.Throws<ArgumentNullException>("routeData", () =>
                    {
                        RedirectorBuilderExtensions.BuildLocation(
                            redirect: Redirect(),
                            routeData: null!);
                    });
                }

                [Theory]
                [InlineData("/one")]
                [InlineData("/two")]
                public void EmptyRouteData_ReturnsLocation(string location)
                {
                    var redirect = new RedirectRoute("/template", location);
                    var routeData = RouteData();

                    var result = RedirectorBuilderExtensions.BuildLocation(redirect,
                        routeData);

                    Assert.Equal(location, result);
                }

                [Fact]
                public void OneRouteDataValue_ReturnsLocation()
                {
                    var redirect = new RedirectRoute("/old/{param1}", "/new/{param1}");
                    var routeData = RouteData();
                    routeData.Values.Add("param1", "value1");

                    var result = RedirectorBuilderExtensions.BuildLocation(redirect,
                        routeData);

                    Assert.Equal("/new/value1", result);
                }

                [Fact]
                public void TwoRouteDataValues_ReturnsLocation()
                {
                    var redirect = new RedirectRoute("/old/{param1}/{param2}",
                        "/new/{param1}/{param2}");
                    var routeData = RouteData();
                    routeData.Values.Add("param1", "value1");
                    routeData.Values.Add("param2", "value2");

                    var result = RedirectorBuilderExtensions.BuildLocation(redirect,
                        routeData);

                    Assert.Equal("/new/value1/value2", result);
                }

                [Fact]
                public void ThreeRouteDataValues_ReturnsLocation()
                {
                    var redirect = new RedirectRoute("/old/{param1}/{param2}/{param3}",
                        "/new/{param1}/{param2}/{param3}");
                    var routeData = RouteData();
                    routeData.Values.Add("param1", "value1");
                    routeData.Values.Add("param2", "value2");
                    routeData.Values.Add("param3", "value3");

                    var result = RedirectorBuilderExtensions.BuildLocation(redirect,
                        routeData);

                    Assert.Equal("/new/value1/value2/value3", result);
                }
            }
        }

        private static IApplicationBuilder App()
        {
            var services = new ServiceCollection().AddRouting();

            return new ApplicationBuilder(services.BuildServiceProvider());
        }
        private static RedirectRoute[] Redirects() => new RedirectRoute[0];
        private static RedirectRoute Redirect(bool permanent = false) =>
            new RedirectRoute("/template", "/location", permanent);
        private static RouteData RouteData() => new RouteData();
    }
}