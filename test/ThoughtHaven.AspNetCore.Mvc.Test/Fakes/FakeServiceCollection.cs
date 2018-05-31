using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
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
            this.AddSingleton<IHostingEnvironment, FakeHostingEnvironment>();
            this.AddSingleton<DiagnosticSource, FakeDiagnosticSource>();
        }
    }
}