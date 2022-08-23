using Microsoft.Extensions.Logging;

namespace SmartFamily.Logging;

public class CustomLogger : ILogger, IDisposable
{
    public List<LogLevel> LogLevels => new()
    {
        LogLevel.Information,
        LogLevel.Warning,
        LogLevel.Debug
    };

    private string _categoryName;

    public CustomLogger(string categoryName)
    {
        _categoryName = categoryName;
    }

    public IDisposable BeginScope<TState>(TState state)
        => this;

    public bool IsEnabled(LogLevel logLevel)
        => LogLevels.Contains(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        // Log the event
    }

    public void Dispose()
    {
        // Dispose
    }
}