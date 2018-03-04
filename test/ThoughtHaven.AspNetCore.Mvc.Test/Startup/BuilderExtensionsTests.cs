using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Startup
{
    public class BuilderExtensionsTests
    {
        public class UseThoughtHavenMvcMethod
        {
            public class AppAndEnvironmentAndOptionsOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        ((IApplicationBuilder)null).UseThoughtHavenMvc(
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
                            environment: null,
                            options: Options());
                    });
                }
                
                [Fact]
                public void EnvironmentIsDevelopment_CallsUseDeveloperExceptionPage()
                {
                    var environment = Environment();
                    environment.EnvironmentName = "Development";
                    var options = Options();

                    App().UseThoughtHavenMvc(environment, options);

                    Assert.True(options.GetDeveloperExceptionPage_Called);
                }

                [Fact]
                public void EnvironmentIsNotDevelopment_CallsUseDeveloperExceptionPage()
                {
                    var environment = Environment();
                    environment.EnvironmentName = "Test";
                    var options = Options();

                    App().UseThoughtHavenMvc(environment, options);

                    Assert.True(options.GetExceptionHandler_Called);
                }

                [Fact]
                public void WhenCalled_CallsUseStatusCodePagesWithReExecute()
                {
                    var options = Options();

                    App().UseThoughtHavenMvc(Environment(), options);

                    Assert.True(options.GetStatusCodePagePathFormat_Called);
                }

                [Fact]
                public void WhenCalled_CallsUseStaticFiles()
                {
                    var options = Options();

                    App().UseThoughtHavenMvc(Environment(), options);

                    Assert.True(options.GetStaticFiles_Called);
                }
            }
        }

        private static FakeServiceCollection Services() => new FakeServiceCollection();
        private static IApplicationBuilder App()
        {
            var services = Services();
            services.AddThoughtHavenMvc();

            return new ApplicationBuilder(services.BuildServiceProvider());
        }
        private static FakeHostingEnvironment Environment() => new FakeHostingEnvironment();
        private static FakeBuilderOptions Options() => new FakeBuilderOptions();
    }
}