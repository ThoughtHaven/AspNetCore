using Microsoft.AspNetCore.Http;
using System;
using ThoughtHaven.AspNetCore.Http;

namespace ThoughtHaven.AspNetCore.Fakes
{
    public class FakeWebAppPagesBase : WebAppPagesBase
    {
        public FakeWebAppPagesBase(Uri baseUri) : base(baseUri) { }

        new public Uri BuildUri(string path, QueryString query = default) =>
            base.BuildUri(path, query);

        new public Uri BuildUri(PathString path, QueryString query = default) =>
            base.BuildUri(path, query);
    }
}