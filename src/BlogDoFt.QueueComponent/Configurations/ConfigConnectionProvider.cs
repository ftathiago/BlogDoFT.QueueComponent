using System.Collections.Generic;

namespace BlogDoFt.QueueComponent.Configurations
{
    public sealed class ConfigConnectionProvider
    {
        private static ConfigConnectionProvider _singletonInstance;

        private readonly Dictionary<string, QueueConnConfiguration> _configurations = new Dictionary<string, QueueConnConfiguration>();

        private ConfigConnectionProvider()
        {
        }

        internal static ConfigConnectionProvider NewInstance() => _singletonInstance ??= new ConfigConnectionProvider();

        internal QueueConnConfiguration GetConfigTo<TClass>()
        {
            _configurations.TryGetValue(GetKey<TClass>(), out var configuration);
            return configuration;
        }

        internal void SetConfigTo<TClass>(QueueConnConfiguration config)
        {
            var key = GetKey<TClass>();
            _configurations.Remove(key);
            _configurations.Add(key, config);
        }

        private string GetKey<TClass>() => typeof(TClass).FullName;
    }
}
