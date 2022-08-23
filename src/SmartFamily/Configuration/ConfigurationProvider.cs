using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace SmartFamily.Configuration;

public class ConfigurationProvider : IConfigurationProvider
{
    public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
    {
        return Enumerable.Empty<string>();
    }

    public IChangeToken GetReloadToken()
        => default;

    public void Load()
    {
    }

    public void Set(string key, string value)
    {
        Preferences.Set(key, value);
    }

    public bool TryGet(string key, out string value)
    {
        value = Preferences.Get(key, string.Empty);

        return Preferences.ContainsKey(key);
    }
}