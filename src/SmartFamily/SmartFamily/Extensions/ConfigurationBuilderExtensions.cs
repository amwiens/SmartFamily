using Microsoft.Extensions.Configuration;

using SmartFamily.Configuration;

namespace SmartFamily.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddPreferences(this IConfigurationBuilder builder, IEnumerable<string> availableKeys)
    {
        builder.Add(new ConfigurationSource(availableKeys));

        return builder;
    }
}