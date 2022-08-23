using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using SmartFamily.Logging;

namespace SmartFamily.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

        return builder;
    }
}