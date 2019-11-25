using System;
using Xunit;

namespace Microsoft.AspNetCore.Builder
{
    public class SecurityHeaderBuilderExtensionsTests
    {
        public class UseSecurityHeadersMethod
        {
            public class AppAndOptionsOverload
            {
                [Fact]
                public void NullApp_Throws()
                {
                    Assert.Throws<ArgumentNullException>("app", () =>
                    {
                        SecurityHeaderBuilderExtensions.UseSecurityHeaders(app: null!);
                    });
                }
            }
        }
    }
}