using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using ThoughtHaven;

namespace Microsoft.AspNetCore.Builder
{
    public static class ThoughtHavenMvcBuilderExtensions
    {
        public static IApplicationBuilder UseThoughtHavenMvc(this IApplicationBuilder app,
            IWebHostEnvironment environment, string iisUrlRewriteFilePath,
            Action<IEndpointRouteBuilder>? configureRoutes = null)
        {
            Guard.Null(nameof(app), app);
            Guard.Null(nameof(environment), environment);
            Guard.NullOrWhiteSpace(nameof(iisUrlRewriteFilePath), iisUrlRewriteFilePath);

            var options = new MvcBuilderOptions();
            options.Rewrite.IISUrlRewriteFilePath = iisUrlRewriteFilePath;

            return app.UseThoughtHavenMvc(environment, options, configureRoutes);
        }

        public static IApplicationBuilder UseThoughtHavenMvc(this IApplicationBuilder app,
            IWebHostEnvironment environment, MvcBuilderOptions? options = null,
            Action<IEndpointRouteBuilder>? configureRoutes = null)
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
            }

            if (!string.IsNullOrWhiteSpace(options.Rewrite.IISUrlRewriteFilePath))
            {
                string baseDir = environment.ContentRootPath;
                var filePath = Path.Combine(baseDir, options.Rewrite.IISUrlRewriteFilePath);

                using var rewrite = File.OpenText(filePath);

                app.UseRewriter(new RewriteOptions().AddIISUrlRewrite(rewrite));
            }

            app.UseHttpsRedirection();
            app.UseSecurityHeaders(options.SecurityHeaders);
            app.UseStatusCodePagesWithReExecute(options.StatusCodePagePathFormat);
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseTrackingConsent();

            app.UseEndpoints(endpoints =>
            {
                if (configureRoutes == null)
                {
                    endpoints.MapControllers();
                }
                else
                {
                    configureRoutes(endpoints);
                }
            });

            return app;
        }
    }
}