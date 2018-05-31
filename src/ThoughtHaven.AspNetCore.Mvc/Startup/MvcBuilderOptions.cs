using ThoughtHaven;
using static Microsoft.Net.Http.Headers.HeaderNames;

namespace Microsoft.AspNetCore.Builder
{
    public class MvcBuilderOptions
    {
        public virtual DeveloperExceptionPageOptions DeveloperExceptionPage { get; } =
            new DeveloperExceptionPageOptions();

        public virtual ExceptionHandlerOptions ExceptionHandler { get; } =
            new ExceptionHandlerOptions() { ExceptionHandlingPath = "/errors/server" };

        private string _statusCodePagePathFormat = "/errors/statuscode/{0}";
        public virtual string StatusCodePagePathFormat
        {
            get => this._statusCodePagePathFormat;
            set => this._statusCodePagePathFormat = Guard.NullOrWhiteSpace(nameof(value),
                value);
        }

        public virtual StaticFileOptions StaticFiles { get; } =
            new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[CacheControl] = "public,max-age=31536000";
                }
            };
    }
}