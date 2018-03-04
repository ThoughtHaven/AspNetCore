using Microsoft.AspNetCore.Http;
using System;

namespace ThoughtHaven.AspNetCore.Routing
{
    public abstract class PagesBase
    {
        protected Uri BaseDomain { get; }

        protected PagesBase(Uri baseDomain)
        {
            this.BaseDomain = Guard.Null(nameof(baseDomain), baseDomain);
        }

        protected virtual Uri BuildUri(string path, QueryString query = default(QueryString))
        {
            Guard.NullOrWhiteSpace(nameof(path), path);

            return new Uri(this.BaseDomain, $"{path}{query}");
        }
    }
}