using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

namespace ThoughtHaven.AspNetCore.Mvc.Ui
{
    public class UiPostConfigureStaticFileOptions<TAssembly>
        : IPostConfigureOptions<StaticFileOptions>
    {
        protected IHostingEnvironment Environment { get; }
        protected virtual string ManifestEmbeddedFileRoot { get; } = "wwwroot";

        public UiPostConfigureStaticFileOptions(IHostingEnvironment environment)
        {
            this.Environment = Guard.Null(nameof(environment), environment);
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            Guard.Null(nameof(options), options);

            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && this.Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider ??= this.Environment.WebRootFileProvider;

            var filesProvider = new ManifestEmbeddedFileProvider(typeof(TAssembly).Assembly,
                this.ManifestEmbeddedFileRoot);
            options.FileProvider = new CompositeFileProvider(options.FileProvider,
                filesProvider);
        }
    }
}