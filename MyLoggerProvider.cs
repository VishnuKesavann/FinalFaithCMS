using Microsoft.Extensions.Logging;
using System;

namespace FinalCMS
{
    public class MyLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) =>
            new MyLogger();

        public void Dispose() { }

        private class MyLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state) => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                Exception exception, Func<TState, Exception, string> formatter)
            {
                if (logLevel == LogLevel.Information)
                    Console.WriteLine(formatter(state, exception));
            }
        }
    }
}
