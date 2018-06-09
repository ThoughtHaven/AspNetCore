using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TrackingConsentOptionsTests
    {
        public class AntiforgeryProperty
        {
            public class GetOperator
            {
                [Fact]
                public void DefaultValue_ReturnsNull()
                {
                    Assert.Null(new TrackingConsentOptions().Antiforgery);
                }
            }

            public class SetOperator
            {
                [Fact]
                public void WhenCalled_UpdatesValue()
                {
                    void action(AntiforgeryOptions o) { }

                    var options = new TrackingConsentOptions()
                    {
                        Antiforgery = action
                    };

                    Assert.Equal(action, options.Antiforgery);
                }
            }
        }

        public class CookiePolicyProperty
        {
            public class GetOperator
            {
                [Fact]
                public void DefaultValue_ReturnsNull()
                {
                    Assert.Null(new TrackingConsentOptions().CookiePolicy);
                }
            }

            public class SetOperator
            {
                [Fact]
                public void WhenCalled_UpdatesValue()
                {
                    void action(CookiePolicyOptions o) { }

                    var options = new TrackingConsentOptions()
                    {
                        CookiePolicy = action
                    };

                    Assert.Equal(action, options.CookiePolicy);
                }
            }
        }
    }
}