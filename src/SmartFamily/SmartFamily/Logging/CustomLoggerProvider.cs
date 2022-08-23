using Microsoft.Extensions.Logging;

namespace SmartFamily.Logging;

public class CustomLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new CustomLogger(categoryName);
    }

    public void Dispose()
    {
    }
}