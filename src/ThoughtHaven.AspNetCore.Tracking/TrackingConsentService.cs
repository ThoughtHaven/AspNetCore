using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.Tracking
{
    public class TrackingConsentService : ITrackingConsentService
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }

        public TrackingConsentService(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public virtual Task<bool> CanTrack()
        {
            var track = this.ConsentFeature(required: false)?.CanTrack ?? false;

            return Task.FromResult(track);
        }

        public virtual Task GrantConsent()
        {
            this.ConsentFeature().GrantConsent();

            return Task.CompletedTask;
        }

        public virtual Task WithdrawConsent()
        {
            this.ConsentFeature().WithdrawConsent();

            return Task.CompletedTask;
        }

        protected ITrackingConsentFeature ConsentFeature() =>
            this.ConsentFeature(required: true)!;

        protected ITrackingConsentFeature? ConsentFeature(bool required)
        {
            var httpContext = this.HttpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                throw new InvalidOperationException($"Operation requires an {nameof(HttpContext)} from the {nameof(IHttpContextAccessor)}.");
            }

            var feature = httpContext!.Features.Get<ITrackingConsentFeature>();

            if (required && feature == null)
            {
                throw new InvalidOperationException($"No {nameof(ITrackingConsentFeature)} exists in the {nameof(HttpContext)}.{nameof(HttpContext.Features)} collection.");
            }

            return feature;
        }
    }
}