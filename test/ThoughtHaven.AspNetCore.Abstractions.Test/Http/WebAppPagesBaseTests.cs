using Microsoft.AspNetCore.Http;
using System;
using ThoughtHaven.AspNetCore.Fakes;
using Xunit;

namespace ThoughtHaven.AspNetCore.Http
{
    public class WebAppPagesBaseTests
    {
        public class Constructor
        {
            public class BaseUriOverload
            {
                [Fact]
                public void NullBaseDomain_Throws()
                {
                    Assert.Throws<ArgumentNullException>("baseUri", () =>
                    {
                        new FakeWebAppPagesBase(baseUri: null);
                    });
                }
            }
        }

        public class BuildUriMethod
        {
            public class StringPathAndQueryOverload
            {
                [Fact]
                public void NullPath_Throws()
                {
                    Assert.Throws<ArgumentNullException>("path", () =>
                    {
                        Pages().BuildUri(path: (string)null);
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
                public void EmptyPath_ReturnsUri()
                {
                    var result = Pages().BuildUri("");

                    Assert.Equal(new Uri("https://example.com/"), result);
                }

                [Fact]
                public void EmptyPathAndQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("", new QueryString("?one=1"));

                    Assert.Equal(new Uri("https://example.com/?one=1"), result);
                }

                [Fact]
                public void RootPath_ReturnsUri()
                {
                    var result = Pages().BuildUri("/");

                    Assert.Equal(new Uri("https://example.com/"), result);
                }

                [Fact]
                public void RootPathAndQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("", new QueryString("?one=1"));

                    Assert.Equal(new Uri("https://example.com/?one=1"), result);
                }

                [Fact]
                public void WithPath_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path");

                    Assert.Equal(new Uri("https://example.com/path"), result);
                }

                [Fact]
                public void WithPathAndQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path", new QueryString("?one=1"));

                    Assert.Equal(new Uri("https://example.com/path?one=1"), result);
                }

                [Fact]
                public void PathHasQuery_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path?one=1");

                    Assert.Equal(new Uri("https://example.com/path?one=1"), result);
                }

                [Fact]
                public void PathHasQueryAndQueryHasValue_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path?one=1",
                        new QueryString("?two=2"));

                    Assert.Equal(new Uri("https://example.com/path?one=1&two=2"), result);
                }

                [Fact]
                public void PathHasEncodedQueryAndQueryHasEncodedValue_ReturnsUri()
                {
                    var result = Pages().BuildUri("/path?encodedone=%2Furl",
                        new QueryString("?encodedtwo=%2Furl"));

                    Assert.Equal(new Uri("https://example.com/path?encodedone=%2Furl&encodedtwo=%2Furl"),
                        result);
                }
            }
        }

        private static FakeWebAppPagesBase Pages() =>
            new FakeWebAppPagesBase(new Uri("https://example.com"));
    }
}