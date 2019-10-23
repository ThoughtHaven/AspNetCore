using Microsoft.Extensions.DependencyInjection;
using System;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using Xunit;

namespace Microsoft.AspNetCore.Builder
{
    public class ThoughtHavenMvcBuilderExtensionsTests
    {
        public class UseThoughtHavenMvcMethod
        {
            public class IisUrlRewriteFilePathOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        ((IApplicationBuilder)null!).UseThoughtHavenMvc(
                            environment: Environment(),
                            iisUrlRewriteFilePath: "some/path");
                    });
                }

                [Fact]
                public void NullEnvironment_Throws()
                {
                    Assert.Throws<ArgumentNullException>("environment", () =>
                    {
                        App().UseThoughtHavenMvc(
                            environment: null!,
                            iisUrlRewriteFilePath: "some/path");
                    });
                }

                [Fact]
                public void NullIisUrlRewriteFilePath_Throws()
                {
                    Assert.Throws<ArgumentNullException>("iisUrlRewriteFilePath", () =>
                    {
                        App().UseThoughtHavenMvc(
                            environment: Environment(),
                            iisUrlRewriteFilePath: null!);
                    });
                }

                [Fact]
                public void EmptyIisUrlRewriteFilePath_Throws()
                {
                    Assert.Throws<ArgumentException>("iisUrlRewriteFilePath", () =>
                    {
                        App().UseThoughtHavenMvc(
                            environment: Environment(),
                            iisUrlRewriteFilePath: "");
                    });
                }

                [Fact]
                public void WhiteSpaceIisUrlRewriteFilePath_Throws()
                {
                    Assert.Throws<ArgumentException>("iisUrlRewriteFilePath", () =>
                    {
                        App().UseThoughtHavenMvc(
                            environment: Environment(),
                            iisUrlRewriteFilePath: " ");
                    });
                }
            }

            public class PrimaryOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        ((IApplicationBuilder)null!).UseThoughtHavenMvc(
                            environment: Environment(),
                            options: Options());
                    });
                }

                [Fact]
                public void NullEnvironment_Throws()
                {
                    Assert.Throws<ArgumentNullException>("environment", () =>
                    {
                        App().UseThoughtHavenMvc(
                            environment: null!,
                            options: Options());
                    });
                }
            }
        }

        private static FakeServiceCollection Services() => new FakeServiceCollection();
        private static IApplicationBuilder App()
        {
            var services = Services();
            services.AddThoughtHavenMvc();

            var app = new ApplicationBuilder(services.BuildServiceProvider());

            return app;
        }
        private static FakeWebHostEnvironment Environment() => new FakeWebHostEnvironment();
        private static FakeBuilderOptions Options() => new FakeBuilderOptions();
    }
}