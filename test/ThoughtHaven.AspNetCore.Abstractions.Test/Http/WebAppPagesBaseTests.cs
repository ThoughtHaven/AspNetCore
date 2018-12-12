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
                public void RootBaseUriWithEmptyPath_ReturnsUri()
                {
                    var result = Pages("https://example.com").BuildUri("");

                    Assert.Equal("https://example.com/", result.ToString());
                }

                [Fact]
                public void PathBaseUriWithEmptyPath_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path").BuildUri("");

                    Assert.Equal("https://example.com/with-path", result.ToString());
                }

                [Fact]
                public void RootBaseUriEmptyPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com")
                        .BuildUri("", new QueryString("?one=1"));

                    Assert.Equal("https://example.com/?one=1", result.ToString());
                }

                [Fact]
                public void PathBaseUriEmptyPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("", new QueryString("?one=1"));

                    Assert.Equal($"https://example.com/with-path?one=1", result.ToString());
                }

                [Fact]
                public void RootBaseUriRootPath_ReturnsUri()
                {
                    var result = Pages("https://example.com").BuildUri("/");

                    Assert.Equal("https://example.com/", result.ToString());
                }

                [Fact]
                public void PathBaseUriRootPath_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path").BuildUri("/");

                    Assert.Equal("https://example.com/with-path", result.ToString());
                }

                [Fact]
                public void RootBaseUriRootPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com")
                        .BuildUri("", new QueryString("?one=1"));

                    Assert.Equal("https://example.com/?one=1", result.ToString());
                }

                [Fact]
                public void PathBaseUriRootPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("", new QueryString("?one=1"));

                    Assert.Equal("https://example.com/with-path?one=1", result.ToString());
                }

                [Fact]
                public void RootBaseUriWithPath_ReturnsUri()
                {
                    var result = Pages("https://example.com").BuildUri("/path");

                    Assert.Equal($"https://example.com/path", result.ToString());
                }

                [Fact]
                public void PathBaseUriWithPath_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path").BuildUri("/path");

                    Assert.Equal($"https://example.com/with-path/path", result.ToString());
                }

                [Fact]
                public void RootBaseUriWithPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com")
                        .BuildUri("/path", new QueryString("?one=1"));

                    Assert.Equal("https://example.com/path?one=1", result.ToString());
                }

                [Fact]
                public void PathBaseUriWithPathAndQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("/path", new QueryString("?one=1"));

                    Assert.Equal("https://example.com/with-path/path?one=1",
                        result.ToString());
                }

                [Fact]
                public void RootBaseUriAndPathHasQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com").BuildUri("/path?one=1");

                    Assert.Equal("https://example.com/path?one=1", result.ToString());
                }

                [Fact]
                public void PathBaseUriAndPathHasQuery_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("/path?one=1");

                    Assert.Equal("https://example.com/with-path/path?one=1",
                        result.ToString());
                }

                [Fact]
                public void RootBaseUriAndPathHasQueryAndQueryHasValue_ReturnsUri()
                {
                    var result = Pages("https://example.com").BuildUri("/path?one=1",
                        new QueryString("?two=2"));

                    Assert.Equal("https://example.com/path?one=1&two=2", result.ToString());
                }

                [Fact]
                public void PathBaseUriAndPathHasQueryAndQueryHasValue_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("/path?one=1", new QueryString("?two=2"));

                    Assert.Equal("https://example.com/with-path/path?one=1&two=2",
                        result.ToString());
                }

                [Fact]
                public void RootBaseUriAndPathHasEncodedQueryAndQueryHasEncodedValue_ReturnsUri()
                {
                    var result = Pages("https://example.com")
                        .BuildUri("/path?encodedone=%2Furl",
                            new QueryString("?encodedtwo=%2Furl"));

                    Assert.Equal("https://example.com/path?encodedone=%2Furl&encodedtwo=%2Furl",
                        result.ToString());
                }

                [Fact]
                public void PathBaseUriAndPathHasEncodedQueryAndQueryHasEncodedValue_ReturnsUri()
                {
                    var result = Pages("https://example.com/with-path")
                        .BuildUri("/path?encodedone=%2Furl",
                            new QueryString("?encodedtwo=%2Furl"));

                    Assert.Equal("https://example.com/with-path/path?encodedone=%2Furl&encodedtwo=%2Furl",
                        result.ToString());
                }
            }
        }

        private static FakeWebAppPagesBase Pages(string baseUri = "https://example.com") =>
            new FakeWebAppPagesBase(new Uri(baseUri));
    }
}