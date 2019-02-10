using Microsoft.AspNetCore.Builder.Internal;
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
                            app: null,
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
                            redirects: null);
                    });
                }

                [Fact]
                public void NullItemInRedirects_Throws()
                {
                    Assert.Throws<ArgumentException>("redirects", () =>
                    {
                        RedirectorBuilderExtensions.UseRedirector(
                            app: App(),
                            redirects: new RedirectRoute[] { null });
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

        private static IApplicationBuilder App()
        {
            var services = new ServiceCollection().AddRouting();

            return new ApplicationBuilder(services.BuildServiceProvider());
        }
        private static RedirectRoute[] Redirects() => new RedirectRoute[0];
    }
}