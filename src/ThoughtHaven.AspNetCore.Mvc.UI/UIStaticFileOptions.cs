using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System;
using System.Reflection;

namespace ThoughtHaven.AspNetCore.Mvc.UI
{
    public class UIStaticFileOptions : StaticFileOptions
    {
        new public ManifestEmbeddedFileProvider FileProvider =>
            (ManifestEmbeddedFileProvider)base.FileProvider;

        public UIStaticFileOptions(Assembly embeddedFileAssembly)
            : this(embeddedFileAssembly, root: "wwwroot")
        { }

        public UIStaticFileOptions(Assembly embeddedFileAssembly, string root)
            : this(new ManifestEmbeddedFileProvider(
                embeddedFileAssembly ?? throw new ArgumentNullException(
                    nameof(embeddedFileAssembly)),
                root ?? throw new ArgumentNullException(nameof(root))))
        { }

        public UIStaticFileOptions(ManifestEmbeddedFileProvider fileProvider)
        {
            base.FileProvider = fileProvider ??
                throw new ArgumentNullException(nameof(fileProvider));
            this.OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                    "public,max-age=31536000";
            };
        }
    }
}