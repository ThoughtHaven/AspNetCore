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

        protected virtual Uri BuildUri(string path, QueryString query = default)
        {
            if (path != string.Empty) { Guard.NullOrWhiteSpace(nameof(path), path); }

            if (path.EndsWith("/")) { path = path.Substring(0, path.Length - 1); }

            var queryIndex = path.IndexOf("?");
            if (queryIndex != -1)
            {
                var pathQueryString = path.Substring(startIndex: queryIndex);
                path = path.Substring(0, length: queryIndex);

                var fullQuery = query.HasValue
                    ? $"{pathQueryString}{query.Value.Replace("?", "&")}"
                    : pathQueryString;

                query = new QueryString(fullQuery);
            }

            return this.BuildUri(new PathString(path), query);
        }

        protected virtual Uri BuildUri(PathString path, QueryString query = default)
        {
            var baseUri = this.BaseUri.ToString();

            if (baseUri.EndsWith("/"))
            {
                baseUri = baseUri.Substring(0, baseUri.Length - 1);
            }

            return new Uri($"{baseUri}{path}{query}");
        }
    }
}