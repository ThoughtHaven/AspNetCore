using System;
using Microsoft.Extensions.Logging;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) { }
        public ILogger CreateLogger(string categoryName) => new Logger();
        public void Dispose() { }

        public class Logger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state) =>
                throw new NotImplementedException();
            public bool IsEnabled(LogLevel logLevel) => true;
            public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception exception,
                Func<TState, Exception, string> formatter) => new object();
        }

        public class Logger<T> : Logger, ILogger<T> { }
    }
}