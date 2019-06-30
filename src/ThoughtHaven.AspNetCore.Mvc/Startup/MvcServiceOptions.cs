using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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
        };
        public Action<RouteOptions> Routing
        {
            get => this._routing;
            set => this._routing = Guard.Null(nameof(value), value);
        }

        private TrackingConsentOptions _trackingConsent = new TrackingConsentOptions();
        public TrackingConsentOptions TrackingConsent
        {
            get => this._trackingConsent;
            set => this._trackingConsent = Guard.Null(nameof(value), value);
        }
    }
}