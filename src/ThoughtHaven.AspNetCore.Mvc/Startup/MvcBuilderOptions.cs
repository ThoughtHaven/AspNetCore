using ThoughtHaven;

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

        public RewriteOptions Rewrite { get; } = new RewriteOptions();

        public class RewriteOptions
        {
            public string? IISUrlRewriteFilePath { get; set; }
        }
    }
}