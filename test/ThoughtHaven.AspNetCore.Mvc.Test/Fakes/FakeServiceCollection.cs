using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeServiceCollection : ServiceCollection
    {
        public FakeServiceCollection() : base()
        {
            this.AddSingleton<ILoggerFactory, FakeLoggerFactory>();
            this.AddSingleton<ObjectPoolProvider, FakeObjectPoolProvider>();
            this.AddSingleton<IWebHostEnvironment, FakeWebHostEnvironment>();
            this.AddSingleton<DiagnosticSource, FakeDiagnosticSource>();
            this.AddSingleton(new DiagnosticListener("Test"));
        }
    }
}