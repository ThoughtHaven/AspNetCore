using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThoughtHaven.AspNetCore.Mvc.UI.Test.EmbeddedFiles;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.UI
{
    public class MvcUIBuilderExtensionsTests
    {
        public class UseUIMethod
        {
            public class AppAndOptionsOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        MvcUIBuilderExtensions.UseUI(
                            app: null,
                            options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        App().UseUI(options: null);
                    });
                }

                [Fact]
                public void MvcServicesNotRegistered_Throws()
                {
                    var exception = Assert.Throws<InvalidOperationException>(() =>
                    {
                        App(registerMvc: false).UseUI(Options());
                    });

                    Assert.Equal("Thought Haven Mvc UI is built on top of ASP.NET Core Mvc. Please add all the required services by calling 'IServiceCollection.AddMvc' inside the call to 'ConfigureServices(...)' in the application startup code.",
                        exception.Message);
                }
            }
        }

        private static IApplicationBuilder App(bool registerMvc = true)
        {
            var services = new ServiceCollection();

            if (registerMvc) { services.AddMvc(); }

            return new ApplicationBuilder(services.BuildServiceProvider());
        }
        private static UIStaticFileOptions Options() =>
            new UIStaticFileOptions(typeof(TestEmbeddedUI).Assembly);
    }
}