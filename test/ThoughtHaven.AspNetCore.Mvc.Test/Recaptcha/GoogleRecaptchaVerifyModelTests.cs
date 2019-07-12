using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaVerifyModelTests
    {
        public class ResponseTokenProperty
        {
            [Fact]
            public void HasFromFormAttribute()
            {
                var attribute = TestHelpers.GetPropertyAttribute<GoogleRecaptchaVerifyModel, FromFormAttribute>(
                    "ResponseToken");

                Assert.Equal("g-recaptcha-response", attribute.Name);
            }
        }
    }
}