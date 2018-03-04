using System;
using Microsoft.AspNetCore.Http;
using ThoughtHaven.AspNetCore.Routing;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakePagesBase : PagesBase
    {
        public FakePagesBase(Uri baseDomain) : base(baseDomain) { }

        new public Uri BuildUri(string path, QueryString query = default(QueryString)) =>
            base.BuildUri(path, query);
    }
}