using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecurityHeaderOptions _options;

        public SecurityHeaderMiddleware(RequestDelegate next, SecurityHeaderOptions options)
        {
            this._next = Guard.Null(nameof(next), next);
            this._options = Guard.Null(nameof(options), options);
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Null(nameof(context), context);

            var headers = context.Response.Headers;

            if (this._options.ContentSecurityPolicy != null)
            {
                headers.Add("content-security-policy", this._options.ContentSecurityPolicy);
            }

            if (this._options.XxsProtection != null)
            {
                headers.Add("x-xss-protection", this._options.XxsProtection);
            }

            if (this._options.XFrameOptions != null)
            {
                headers.Add("x-frame-options", this._options.XFrameOptions);
            }

            if (this._options.XContentTypeOptions != null)
            {
                headers.Add("x-content-type-options", this._options.XContentTypeOptions);
            }

            if (this._options.ReferrerPolicy != null)
            {
                headers.Add("Referrer-Policy", this._options.ReferrerPolicy);
            }

            if (this._options.ExpectCT != null)
            {
                headers.Add("Expect-CT", this._options.ExpectCT);
            }

            if (this._options.FeaturePolicy != null)
            {
                headers.Add("Feature-Policy", this._options.FeaturePolicy);
            }

            await this._next(context);
        }
    }
}