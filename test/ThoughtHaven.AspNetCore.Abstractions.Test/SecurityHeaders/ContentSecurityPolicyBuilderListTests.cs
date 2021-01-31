using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public partial class ContentSecurityPolicyBuilderTests
    {
        public class List
        {
            public class Type
            {
                [Fact]
                public void InheritsListOfString()
                {
                    Assert.True(typeof(List<string>).IsAssignableFrom(
                        typeof(ContentSecurityPolicyBuilder.List)));
                }
            }

            public class AddSelfMethod
            {
                public class EmptyOverload
                {
                    [Fact]
                    public void WhenCalled_AddsSelf()
                    {
                        var list = new ContentSecurityPolicyBuilder.List();

                        list.AddSelf();

                        Assert.Single(list);
                        Assert.Equal("'self'", list.First());
                    }
                }
            }
        }

        public class ImageList
        {
            public class Type
            {
                [Fact]
                public void InheritsList()
                {
                    Assert.True(typeof(ContentSecurityPolicyBuilder.List).IsAssignableFrom(
                        typeof(ContentSecurityPolicyBuilder.ImageList)));
                }
            }

            public class AddDataMethod
            {
                public class EmptyOverload
                {
                    [Fact]
                    public void WhenCalled_AddsData()
                    {
                        var list = new ContentSecurityPolicyBuilder.ImageList();

                        list.AddData();

                        Assert.Single(list);
                        Assert.Equal("data:", list.First());
                    }
                }
            }
        }
    }
}