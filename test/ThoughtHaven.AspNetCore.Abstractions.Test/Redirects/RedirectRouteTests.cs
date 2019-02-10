using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Redirects
{
    public class RedirectRouteTests
    {
        public class Constructor
        {
            public class TemplateAndLocationAndPermanentOverload
            {
                [Fact]
                public void NullTemplate_Throws()
                {
                    Assert.Throws<ArgumentNullException>("template", () =>
                    {
                        new RedirectRoute(
                            template: null,
                            location: "/location");
                    });
                }

                [Fact]
                public void EmptyTemplate_Throws()
                {
                    Assert.Throws<ArgumentException>("template", () =>
                    {
                        new RedirectRoute(
                            template: "",
                            location: "/location");
                    });
                }

                [Fact]
                public void WhiteSpaceTemplate_Throws()
                {
                    Assert.Throws<ArgumentException>("template", () =>
                    {
                        new RedirectRoute(
                            template: " ",
                            location: "/location");
                    });
                }

                [Fact]
                public void NullLocation_Throws()
                {
                    Assert.Throws<ArgumentNullException>("location", () =>
                    {
                        new RedirectRoute(
                            template: "/template",
                            location: null);
                    });
                }

                [Fact]
                public void EmptyLocation_Throws()
                {
                    Assert.Throws<ArgumentException>("location", () =>
                    {
                        new RedirectRoute(
                            template: "/template",
                            location: "");
                    });
                }

                [Fact]
                public void WhiteSpaceLocation_Throws()
                {
                    Assert.Throws<ArgumentException>("location", () =>
                    {
                        new RedirectRoute(
                            template: "/template",
                            location: " ");
                    });
                }

                [Theory]
                [InlineData("/t1")]
                [InlineData("/t2")]
                public void WhenCalled_SetsTemplate(string template)
                {
                    var route = new RedirectRoute(template, "/location");

                    Assert.Equal(template, route.Template);
                }

                [Theory]
                [InlineData("/l1")]
                [InlineData("/l2")]
                public void WhenCalled_SetsLocation(string location)
                {
                    var route = new RedirectRoute("/template", location);

                    Assert.Equal(location, route.Location);
                }

                [Fact]
                public void DefaultPermanet_SetsPermanentToFalse()
                {
                    var route = new RedirectRoute("/template", "/location");

                    Assert.False(route.Permanent);
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                public void WhenCalled_SetsPermanent(bool permanent)
                {
                    var route = new RedirectRoute("/template", "/location", permanent);

                    Assert.Equal(permanent, route.Permanent);
                }
            }
        }
    }
}