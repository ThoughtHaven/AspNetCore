using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace ThoughtHaven.AspNetCore.Tracking.Fakes
{
    public class FakeHttpContextAccessor : IHttpContextAccessor
    {
        public HttpContext? HttpContext { get; set; }

        public FakeHttpContextAccessor(FakeTrackingConsentFeature? trackingFeature = null)
        {
            var features = new FeatureCollection();

            if (trackingFeature != null)
            { features.Set<ITrackingConsentFeature>(trackingFeature); }

            this.HttpContext = new DefaultHttpContext(features);
        }
    }
}