using BlogDoFt.QueueComponent.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogDoFt.QueueComponent.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddQueueSupport(this IServiceCollection services) =>
            services.AddSingleton(_ => ConfigConnectionProvider.NewInstance());

        public static IServiceCollection RegisterConnectionTo<TClass>(
            this IServiceCollection services,
            Action<QueueConnConfiguration> configureQueue)
        {
            var provider = services.ConfigGetConfigConnectionProvider();
            var config = provider.GetConfigTo<TClass>() ?? new QueueConnConfiguration();
            configureQueue(config);
            provider.SetConfigTo<TClass>(config);

            return services;
        }

        private static ConfigConnectionProvider ConfigGetConfigConnectionProvider(
            this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<ConfigConnectionProvider>();
        }
    }
}
