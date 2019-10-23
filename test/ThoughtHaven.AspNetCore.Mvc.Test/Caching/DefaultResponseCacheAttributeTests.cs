using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc
{
    public class DefaultResponseCacheAttributeTests
    {
        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsDuration()
                {
                    var attribute = new DefaultResponseCacheAttribute();

                    Assert.Equal(60 * 5, attribute.Duration);
                }

                [Fact]
                public void WhenCalled_SetsLocation()
                {
                    var attribute = new DefaultResponseCacheAttribute();

                    Assert.Equal(ResponseCacheLocation.Any, attribute.Location);
                }
            }
        }
    }
}