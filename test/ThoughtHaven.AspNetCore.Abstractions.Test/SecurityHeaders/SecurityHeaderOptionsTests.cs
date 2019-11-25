using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public class SecurityHeaderOptionsTests
    {
        public class ContentSecurityPolicyProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("default-src 'self'; img-src 'self' data:; style-src 'self' https://fonts.googleapis.com https://stackpath.bootstrapcdn.com; font-src 'self' https://fonts.gstatic.com https://stackpath.bootstrapcdn.com",
                    options.ContentSecurityPolicy);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.ContentSecurityPolicy = null;

                Assert.Null(options.ContentSecurityPolicy);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.ContentSecurityPolicy = "new";

                Assert.Equal("new", options.ContentSecurityPolicy);
            }
        }

        public class XxsProtectionProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("1; mode=block", options.XxsProtection);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.XxsProtection = null;

                Assert.Null(options.XxsProtection);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.XxsProtection = "new";

                Assert.Equal("new", options.XxsProtection);
            }
        }

        public class XFrameOptionsProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("SAMEORIGIN", options.XFrameOptions);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.XFrameOptions = null;

                Assert.Null(options.XFrameOptions);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.XFrameOptions = "new";

                Assert.Equal("new", options.XFrameOptions);
            }
        }

        public class XContentTypeOptionsProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("nosniff", options.XContentTypeOptions);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.XContentTypeOptions = null;

                Assert.Null(options.XContentTypeOptions);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.XContentTypeOptions = "new";

                Assert.Equal("new", options.XContentTypeOptions);
            }
        }

        public class ReferrerPolicyProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("no-referrer", options.ReferrerPolicy);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.ReferrerPolicy = null;

                Assert.Null(options.ReferrerPolicy);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.ReferrerPolicy = "new";

                Assert.Equal("new", options.ReferrerPolicy);
            }
        }

        public class ExpectCTProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("max-age=86400, enforce", options.ExpectCT);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.ExpectCT = null;

                Assert.Null(options.ExpectCT);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.ExpectCT = "new";

                Assert.Equal("new", options.ExpectCT);
            }
        }

        public class FeaturePolicyProperty
        {
            [Fact]
            public void DefaultValue_Returns()
            {
                var options = Options();

                Assert.Equal("ambient-light-sensor 'none'; accelerometer 'none'; camera 'none'; display-capture 'none'; geolocation 'none'; microphone 'none'; midi 'none'; usb 'none'; wake-lock 'none'; vr 'none'; xr-spatial-tracking 'none'",
                    options.FeaturePolicy);
            }

            [Fact]
            public void SetToNull_ReturnsNull()
            {
                var options = Options();
                options.FeaturePolicy = null;

                Assert.Null(options.FeaturePolicy);
            }

            [Fact]
            public void SetToNewValue_ReturnsNewValue()
            {
                var options = Options();
                options.FeaturePolicy = "new";

                Assert.Equal("new", options.FeaturePolicy);
            }
        }

        private static SecurityHeaderOptions Options() => new SecurityHeaderOptions();
    }
}