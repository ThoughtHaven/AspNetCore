using Microsoft.AspNetCore.Http.Features;
using System;

namespace ThoughtHaven.AspNetCore.Tracking.Fakes
{
    public class FakeTrackingConsentFeature : ITrackingConsentFeature
    {
        public bool IsConsentNeeded => throw new NotImplementedException();

        public bool HasConsent => throw new NotImplementedException();

        public bool CanTrack { get; set; }

        public string CreateConsentCookie() => throw new NotImplementedException();

        public bool GrantConsent_Called;
        public void GrantConsent() => this.GrantConsent_Called = true;

        public bool WithdrawConsent_Called;
        public void WithdrawConsent() => this.WithdrawConsent_Called = true;
    }
}