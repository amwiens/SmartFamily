using Microsoft.Extensions.Configuration;

namespace SmartFamily.Configuration;

public class ConfigurationSource : IConfigurationSource
{
    private IEnumerable<string> _availableKeys;

    public ConfigurationSource(IEnumerable<string> availableKeys)
    {
        _availableKeys = availableKeys;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        Initialize();

        return new ConfigurationProvider();
    }

    private void Initialize()
    {
        if (_availableKeys is { })
        {
            foreach (var key in _availableKeys)
            {
                if (!Preferences.ContainsKey(key))
                {
                    Preferences.Set(key, string.Empty);
                }
            }
        }
    }
}