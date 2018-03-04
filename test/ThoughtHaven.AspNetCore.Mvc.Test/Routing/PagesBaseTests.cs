using Microsoft.AspNetCore.Http;
using System;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Routing
{
    public class PagesBaseTests
    {
        public class Constructor
        {
            public class BaseDomainOverload
            {
                [Fact]
                public void NullBaseDomain_Throws()
                {
                    Assert.Throws<ArgumentNullException>("baseDomain", () =>
                    {
                        new FakePagesBase(baseDomain: null);
                    });
                }
            }
        }

        public class BuildUriMethod
        {
            public class PathAndQueryOverload
            {
                [Fact]
                public void NullPath_Throws()
                {
                    Assert.Throws<ArgumentNullException>("path", () =>
                    {
                        Pages().BuildUri(path: null);
                    });
                }

                [Fact]
                public void EmptyPath_Throws()
                {
                    Assert.Throws<ArgumentException>("path", () =>
                    {
                        Pages().BuildUri(path: "");
                    });
                }

                [Fact]
                public void WhiteSpacePath_Throws()
                {
                    Assert.Throws<ArgumentException>("path", () =>
                    {
                        Pages().BuildUri(path: " ");
                    });
                }

                [Fact]
                public void WithRootPath_ReturnsUri()
                {
                    var result = Pages().BuildUri("/");

                    Assert.Equal(new Uri("https://example.com/"), result);
                }

                [Fact]
                public void WithPath_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path");

                    Assert.Equal(new Uri("https://example.com/path"), result);
                }

                [Fact]
                public void WithRootPathAndQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("/", new QueryString("?one=1"));

                    Assert.Equal(new Uri("https://example.com/?one=1"), result);
                }

                [Fact]
                public void WithPathAndQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path", new QueryString("?one=1"));

                    Assert.Equal(new Uri("https://example.com/path?one=1"), result);
                }
            }
        }

        private static FakePagesBase Pages() =>
            new FakePagesBase(new Uri("https://example.com"));
    }
}