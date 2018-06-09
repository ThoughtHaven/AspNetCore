using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class TrackingConsentOptions
    {
        public Action<AntiforgeryOptions> Antiforgery { get; set; }
        public Action<CookiePolicyOptions> CookiePolicy { get; set; }
    }
}