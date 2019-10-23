using Microsoft.AspNetCore.Mvc.Razor;
using ThoughtHaven;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RazorViewEngineOptionsExtensions
    {
        public static void AddPartialFolderViewLocations(this RazorViewEngineOptions razor)
        {
            Guard.Null(nameof(razor), razor);

            razor.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/Partials/{0}.cshtml");

            razor.ViewLocationFormats.Add("/Views/{1}/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");
        }

        public static void AddRootFolderViewLocations(this RazorViewEngineOptions razor)
        {
            Guard.Null(nameof(razor), razor);

            razor.AreaViewLocationFormats.Insert(0, "/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/{2}/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/{2}/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/{2}/{1}/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/{2}/{1}/{0}.cshtml");

            razor.ViewLocationFormats.Insert(0, "/Shared/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/Shared/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/{1}/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/{1}/{0}.cshtml");
        }

        public static void AddFeaturesFolderViewLocations(this RazorViewEngineOptions razor)
        {
            Guard.Null(nameof(razor), razor);

            razor.AreaViewLocationFormats.Insert(0, "/Features/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Features/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Features/{2}/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Features/{2}/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Features/{2}/{1}/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Insert(0, "/Features/{2}/{1}/{0}.cshtml");

            razor.ViewLocationFormats.Insert(0, "/Features/Shared/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/Features/Shared/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/Features/{1}/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Insert(0, "/Features/{1}/{0}.cshtml");
        }
    }
}