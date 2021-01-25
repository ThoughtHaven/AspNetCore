namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public class SecurityHeaderOptions
    {
        public string? ContentSecurityPolicy { get; set; } = new ContentSecurityPolicyBuilder().ToString();
        public string? XxsProtection { get; set; } = "1; mode=block";
        public string? XFrameOptions { get; set; } = "SAMEORIGIN";
        public string? XContentTypeOptions { get; set; } = "nosniff";
        public string? ReferrerPolicy { get; set; } = "no-referrer";
        public string? ExpectCT { get; set; } = "max-age=86400, enforce";
        public string? FeaturePolicy { get; set; } = "ambient-light-sensor 'none'; accelerometer 'none'; camera 'none'; display-capture 'none'; geolocation 'none'; microphone 'none'; midi 'none'; usb 'none'; wake-lock 'none'; vr 'none'; xr-spatial-tracking 'none'";
    }
}