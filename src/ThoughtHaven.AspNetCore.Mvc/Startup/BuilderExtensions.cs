using Microsoft.AspNetCore.Hosting;
using ThoughtHaven;

namespace Microsoft.AspNetCore.Builder
{
    public static class ThoughtHavenMvcBuilderExtensions
    {
        public static IApplicationBuilder UseThoughtHavenMvc(this IApplicationBuilder app,
            IHostingEnvironment environment, MvcBuilderOptions options = null)
        {
            Guard.Null(nameof(app), app);
            Guard.Null(nameof(environment), environment);

            options = options ?? new MvcBuilderOptions();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(options.DeveloperExceptionPage);
            }
            else
            {
                app.UseExceptionHandler(options.ExceptionHandler);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute(options.StatusCodePagePathFormat);
            app.UseStaticFiles(options.StaticFiles);
            app.UseCookiePolicy();

            return app.UseMvc();
        }
    }
}