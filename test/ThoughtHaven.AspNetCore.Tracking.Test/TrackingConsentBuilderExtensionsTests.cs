using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Microsoft.AspNetCore.Builder
{
    public class TrackingConsentBuilderExtensionsTests
    {
        public class UseTrackingConsentMethod
        {
            public class AppOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        TrackingConsentBuilderExtensions.UseTrackingConsent(app: null!);
                    });
                }

                [Fact]
                public void WhenCalled_ReturnsApp()
                {
                    var app = App();

                    var result = app.UseTrackingConsent();

                    Assert.Equal(app, result);
                }
            }
        }

        private static ApplicationBuilder App() =>
            new ApplicationBuilder(new ServiceCollection().BuildServiceProvider());
    }
}