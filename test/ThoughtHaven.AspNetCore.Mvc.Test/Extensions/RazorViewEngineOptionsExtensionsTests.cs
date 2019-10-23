using Microsoft.AspNetCore.Mvc.Razor;
using System;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class RazorViewEngineOptionsExtensionsTests
    {
        public class AddPartialFolderViewLocationsMethod
        {
            public class RazorOverload
            {
                [Fact]
                public void NullRazor_Throws()
                {
                    Assert.Throws<ArgumentNullException>("razor", () =>
                    {
                        RazorViewEngineOptionsExtensions.AddPartialFolderViewLocations(
                            razor: null!);
                    });
                }

                [Fact]
                public void WhenCalled_AddsAreaViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.AreaViewLocationFormats.Clear();

                    razor.AddPartialFolderViewLocations();

                    Assert.Equal(2, razor.AreaViewLocationFormats.Count);
                    Assert.Equal("/Areas/{2}/Views/{1}/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[0]);
                    Assert.Equal("/Areas/{2}/Views/Shared/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[1]);
                }

                [Fact]
                public void WhenCalled_AddsViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.ViewLocationFormats.Clear();

                    razor.AddPartialFolderViewLocations();

                    Assert.Equal(2, razor.ViewLocationFormats.Count);
                    Assert.Equal("/Views/{1}/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[0]);
                    Assert.Equal("/Views/Shared/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[1]);
                }
            }
        }

        public class AddRootFolderViewLocationsMethod
        {
            public class RazorOverload
            {
                [Fact]
                public void NullRazor_Throws()
                {
                    Assert.Throws<ArgumentNullException>("razor", () =>
                    {
                        RazorViewEngineOptionsExtensions.AddRootFolderViewLocations(
                            razor: null!);
                    });
                }

                [Fact]
                public void WhenCalled_AddsAreaViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.AreaViewLocationFormats.Clear();

                    razor.AddRootFolderViewLocations();

                    Assert.Equal(6, razor.AreaViewLocationFormats.Count);
                    Assert.Equal("/{2}/{1}/{0}.cshtml",
                        razor.AreaViewLocationFormats[0]);
                    Assert.Equal("/{2}/{1}/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[1]);
                    Assert.Equal("/{2}/Shared/{0}.cshtml",
                        razor.AreaViewLocationFormats[2]);
                    Assert.Equal("/{2}/Shared/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[3]);
                    Assert.Equal("/Shared/{0}.cshtml",
                        razor.AreaViewLocationFormats[4]);
                    Assert.Equal("/Shared/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[5]);
                }

                [Fact]
                public void WhenCalled_AddsViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.ViewLocationFormats.Clear();

                    razor.AddRootFolderViewLocations();

                    Assert.Equal(4, razor.ViewLocationFormats.Count);
                    Assert.Equal("/{1}/{0}.cshtml",
                        razor.ViewLocationFormats[0]);
                    Assert.Equal("/{1}/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[1]);
                    Assert.Equal("/Shared/{0}.cshtml",
                        razor.ViewLocationFormats[2]);
                    Assert.Equal("/Shared/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[3]);
                }
            }
        }

        public class AddFeaturesFolderViewLocationsMethod
        {
            public class RazorOverload
            {
                [Fact]
                public void NullRazor_Throws()
                {
                    Assert.Throws<ArgumentNullException>("razor", () =>
                    {
                        RazorViewEngineOptionsExtensions.AddFeaturesFolderViewLocations(
                            razor: null!);
                    });
                }

                [Fact]
                public void WhenCalled_AddsAreaViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.AreaViewLocationFormats.Clear();

                    razor.AddFeaturesFolderViewLocations();

                    Assert.Equal(6, razor.AreaViewLocationFormats.Count);
                    Assert.Equal("/Features/{2}/{1}/{0}.cshtml",
                        razor.AreaViewLocationFormats[0]);
                    Assert.Equal("/Features/{2}/{1}/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[1]);
                    Assert.Equal("/Features/{2}/Shared/{0}.cshtml",
                        razor.AreaViewLocationFormats[2]);
                    Assert.Equal("/Features/{2}/Shared/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[3]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        razor.AreaViewLocationFormats[4]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        razor.AreaViewLocationFormats[5]);
                }

                [Fact]
                public void WhenCalled_AddsViewLocationFormats()
                {
                    var razor = new RazorViewEngineOptions();
                    razor.ViewLocationFormats.Clear();

                    razor.AddFeaturesFolderViewLocations();

                    Assert.Equal(4, razor.ViewLocationFormats.Count);
                    Assert.Equal("/Features/{1}/{0}.cshtml",
                        razor.ViewLocationFormats[0]);
                    Assert.Equal("/Features/{1}/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[1]);
                    Assert.Equal("/Features/Shared/{0}.cshtml",
                        razor.ViewLocationFormats[2]);
                    Assert.Equal("/Features/Shared/Partials/{0}.cshtml",
                        razor.ViewLocationFormats[3]);
                }
            }
        }
    }
}