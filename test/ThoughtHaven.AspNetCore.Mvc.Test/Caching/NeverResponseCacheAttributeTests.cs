using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc
{
    public class NeverResponseCacheAttributeTests
    {
        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsLocation()
                {
                    var attribute = new NeverResponseCacheAttribute();

                    Assert.Equal(ResponseCacheLocation.None, attribute.Location);
                }

                [Fact]
                public void WhenCalled_SetsNoStore()
                {
                    var attribute = new NeverResponseCacheAttribute();

                    Assert.True(attribute.NoStore);
                }
            }
        }
    }
}