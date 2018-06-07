using System;
using Microsoft.AspNetCore.Http;

namespace ThoughtHaven.AspNetCore.Http
{
    public abstract class WebAppPagesBase
    {
        protected Uri BaseUri { get; }

        protected WebAppPagesBase(Uri baseUri)
        {
            this.BaseUri = Guard.Null(nameof(baseUri), baseUri);
        }

        protected virtual Uri BuildUri(string path, QueryString query = default(QueryString))
        {
            if (path != string.Empty) { Guard.NullOrWhiteSpace(nameof(path), path); }

            if (path.EndsWith("/")) { path = path.Substring(0, path.Length - 1); }

            return this.BuildUri(new PathString(path), query);
        }

        protected virtual Uri BuildUri(PathString path,
            QueryString query = default(QueryString)) =>
            new Uri(this.BaseUri, $"{path}{query}");
    }
}