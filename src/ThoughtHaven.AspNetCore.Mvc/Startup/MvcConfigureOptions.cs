using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using ThoughtHaven;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MvcConfigureOptions
    {
        private Action<MvcOptions> _mvc = options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            options.Filters.Add(new RequireHttpsAttribute()
            {
                Permanent = true,
            });
            options.RequireHttpsPermanent = true;
        };
        public Action<MvcOptions> Mvc
        {
            get => this._mvc;
            set { this._mvc = Guard.Null(nameof(value), value); }
        }

        private Action<MvcJsonOptions> _json = json =>
        {
            json.SerializerSettings.Formatting = Formatting.None;
            json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        };
        public Action<MvcJsonOptions> Json
        {
            get => this._json;
            set { this._json = Guard.Null(nameof(value), value); }
        }

        private Action<RazorViewEngineOptions> _razor = razor =>
        {
            razor.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/{2}/{1}/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/{2}/{1}/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/{2}/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/{2}/Shared/Partials/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
            razor.AreaViewLocationFormats.Add("/Features/Shared/Partials/{0}.cshtml");

            razor.ViewLocationFormats.Add("/Views/{1}/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");

            razor.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
            razor.ViewLocationFormats.Add("/Features/{1}/Partials/{0}.cshtml");
            razor.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
            razor.ViewLocationFormats.Add("/Features/Shared/Partials/{0}.cshtml");
        };
        public Action<RazorViewEngineOptions> Razor
        {
            get => this._razor;
            set { this._razor = Guard.Null(nameof(value), value); }
        }

        private Action<MvcViewOptions> _views = views =>
        {
            views.HtmlHelperOptions.ClientValidationEnabled = false;
        };
        public Action<MvcViewOptions> Views
        {
            get => this._views;
            set { this._views = Guard.Null(nameof(value), value); }
        }

        private Action<AntiforgeryOptions> _antiforgery = antiforgery =>
        {
            string xsrf = ".xsrf";

            antiforgery.FormFieldName = xsrf;
            antiforgery.HeaderName = xsrf;
            antiforgery.Cookie.Name = xsrf;
            antiforgery.Cookie.HttpOnly = true;
            antiforgery.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            antiforgery.SuppressXFrameOptionsHeader = false;
        };
        public Action<AntiforgeryOptions> Antiforgery
        {
            get => this._antiforgery;
            set { this._antiforgery = Guard.Null(nameof(value), value); }
        }

        private Action<RouteOptions> _routing = routing =>
        {
            routing.AppendTrailingSlash = false;
            routing.LowercaseUrls = true;
        };
        public Action<RouteOptions> Routing
        {
            get => this._routing;
            set { this._routing = Guard.Null(nameof(value), value); }
        }
    }
}