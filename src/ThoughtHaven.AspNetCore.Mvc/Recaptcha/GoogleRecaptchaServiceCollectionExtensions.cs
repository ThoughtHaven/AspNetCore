using ThoughtHaven;
using ThoughtHaven.AspNetCore.Mvc.Recaptcha;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoogleRecaptchaServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleRecaptchaService(
            this IServiceCollection services, GoogleRecaptchaOptions options)
        {
            Guard.Null(nameof(services), services);
            Guard.Null(nameof(options), options);

            services.AddSingleton(options);
            services.AddHttpClient<GoogleRecaptchaService>();

            return services;
        }
    }
}