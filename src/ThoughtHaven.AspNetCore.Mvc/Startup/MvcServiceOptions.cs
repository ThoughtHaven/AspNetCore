using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Text.Json;
using ThoughtHaven;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MvcServiceOptions
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
            set => this._mvc = Guard.Null(nameof(value), value);
        }

        private Action<JsonOptions> _json = json =>
        {
            json.JsonSerializerOptions.WriteIndented = false;
            json.JsonSerializerOptions.IgnoreNullValues = true;
            json.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        };
        public Action<JsonOptions> Json
        {
            get => this._json;
            set => this._json = Guard.Null(nameof(value), value);
        }

        private Action<RazorViewEngineOptions> _razor = razor =>
        {
            razor.AddPartialFolderViewLocations();
            razor.AddRootFolderViewLocations();
            razor.AddFeaturesFolderViewLocations();
        };
        public Action<RazorViewEngineOptions> Razor
        {
            get => this._razor;
            set => this._razor = Guard.Null(nameof(value), value);
        }

        private Action<MvcViewOptions> _views = views =>
        {
            views.HtmlHelperOptions.ClientValidationEnabled = false;
        };
        public Action<MvcViewOptions> Views
        {
            get => this._views;
            set => this._views = Guard.Null(nameof(value), value);
        }

        private Action<RouteOptions> _routing = routing =>
        {
            routing.AppendTrailingSlash = false;
            routing.LowercaseUrls = true;
            routing.LowercaseQueryStrings = false;
        };
        public Action<RouteOptions> Routing
        {
            get => this._routing;
            set => this._routing = Guard.Null(nameof(value), value);
        }

        private Action<StaticFileOptions> _staticFiles = staticFiles =>
        {
            var prepareResponse = staticFiles.OnPrepareResponse;

            staticFiles.OnPrepareResponse = context =>
            {
                prepareResponse?.Invoke(context);

                context.Context.Response.Headers[HeaderNames.CacheControl] =
                    "public,max-age=31536000";
            };
        };
        public Action<StaticFileOptions> StaticFiles
        {
            get => this._staticFiles;
            set => this._staticFiles = Guard.Null(nameof(value), value);
        }

        private TrackingConsentOptions _trackingConsent = new TrackingConsentOptions();
        public TrackingConsentOptions TrackingConsent
        {
            get => this._trackingConsent;
            set => this._trackingConsent = Guard.Null(nameof(value), value);
        }
    }
}