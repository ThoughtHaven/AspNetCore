using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaKeysTests
    {
        public class Constructor
        {
            public class SiteKeyAndSecretKeyOverload
            {
                [Fact]
                public void NullSiteKey_Throws()
                {
                    Assert.Throws<ArgumentNullException>("siteKey", () =>
                    {
                        new GoogleRecaptchaKeys(
                            siteKey: null!,
                            secretKey: "secret");
                    });
                }

                [Fact]
                public void EmptySiteKey_Throws()
                {
                    Assert.Throws<ArgumentException>("siteKey", () =>
                    {
                        new GoogleRecaptchaKeys(
                            siteKey: "",
                            secretKey: "secret");
                    });
                }

                [Fact]
                public void WhiteSpaceSiteKey_Throws()
                {
                    Assert.Throws<ArgumentException>("siteKey", () =>
                    {
                        new GoogleRecaptchaKeys(
                            siteKey: " ",
                            secretKey: "secret");
                    });
                }

                [Theory]
                [InlineData("key1")]
                [InlineData("key2")]
                public void WhenCalled_SetsSiteKey(string siteKey)
                {
                    var keys = new GoogleRecaptchaKeys(siteKey, "secret");

                    Assert.Equal(siteKey, keys.SiteKey);
                }

                [Theory]
                [InlineData("key1")]
                [InlineData("key2")]
                public void WhenCalled_SetsSecretKey(string secretKey)
                {
                    var keys = new GoogleRecaptchaKeys("site", secretKey);

                    Assert.Equal(secretKey, keys.SecretKey);
                }
            }
        }
    }
}