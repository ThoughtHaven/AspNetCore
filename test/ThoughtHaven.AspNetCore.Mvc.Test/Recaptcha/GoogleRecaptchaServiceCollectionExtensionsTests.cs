using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaServiceCollectionExtensionsTests
    {
        public class AddGoogleRecaptchaServiceMethod
        {
            public class PrimaryOverload
            {
                [Fact]
                public void NullServices_Throws()
                {
                    Assert.Throws<ArgumentNullException>("services", () =>
                    {
                        GoogleRecaptchaServiceCollectionExtensions.AddGoogleRecaptchaService(
                            services: null!,
                            options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        GoogleRecaptchaServiceCollectionExtensions.AddGoogleRecaptchaService(
                            services: Services(),
                            options: null!);
                    });
                }

                [Fact]
                public void WhenCalled_AddsOptions()
                {
                    var services = Services();
                    var options = Options();

                    var result = services.AddGoogleRecaptchaService(options);

                    var service = result.BuildServiceProvider()
                        .GetRequiredService<GoogleRecaptchaOptions>();

                    Assert.Equal(options, service);
                }

                [Fact]
                public void WhenCalled_AddsGoogleRecaptchaService()
                {
                    var services = Services();
                    var options = Options();

                    var result = services.AddGoogleRecaptchaService(options);

                    var service = result.BuildServiceProvider()
                        .GetRequiredService<GoogleRecaptchaService>();

                    Assert.NotNull(service);
                }
            }
        }

        private static ServiceCollection Services()
        {
            var services = new ServiceCollection();

            return services;
        }
        private static GoogleRecaptchaOptions Options() =>
            new GoogleRecaptchaOptions(
                new GoogleRecaptchaKeys("csite", "csecret"),
                new GoogleRecaptchaKeys("isite", "isecret"),
                new GoogleRecaptchaKeys("3site", "3secret"));
    }
}