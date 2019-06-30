using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using ThoughtHaven;

namespace Microsoft.AspNetCore.Builder
{
    public static class ThoughtHavenMvcBuilderExtensions
    {
        public static IApplicationBuilder UseThoughtHavenMvc(this IApplicationBuilder app,
            IHostingEnvironment environment, string iisUrlRewriteFilePath,
            Action<IRouteBuilder>? configureRoutes = null)
        {
            Guard.Null(nameof(app), app);
            Guard.Null(nameof(environment), environment);
            Guard.NullOrWhiteSpace(nameof(iisUrlRewriteFilePath), iisUrlRewriteFilePath);

            var options = new MvcBuilderOptions();
            options.Rewrite.IISUrlRewriteFilePath = iisUrlRewriteFilePath;

            return app.UseThoughtHavenMvc(environment, options, configureRoutes);
        }

        public static IApplicationBuilder UseThoughtHavenMvc(this IApplicationBuilder app,
            IHostingEnvironment environment, MvcBuilderOptions? options = null,
            Action<IRouteBuilder>? configureRoutes = null)
        {
            Guard.Null(nameof(app), app);
            Guard.Null(nameof(environment), environment);

            options ??= new MvcBuilderOptions();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(options.DeveloperExceptionPage);
            }
            else
            {
                app.UseExceptionHandler(options.ExceptionHandler);
                app.UseHsts();
            }

            if (!string.IsNullOrWhiteSpace(options.Rewrite.IISUrlRewriteFilePath))
            {
                string baseDir = environment.ContentRootPath;
                var filePath = Path.Combine(baseDir, options.Rewrite.IISUrlRewriteFilePath);

                using (var rewrite = File.OpenText(filePath))
                {
                    app.UseRewriter(new RewriteOptions().AddIISUrlRewrite(rewrite));
                }
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute(options.StatusCodePagePathFormat);
            app.UseStaticFiles(options.StaticFiles);
            app.UseTrackingConsent();

            return configureRoutes != null ? app.UseMvc(configureRoutes) : app.UseMvc();
        }
    }
}