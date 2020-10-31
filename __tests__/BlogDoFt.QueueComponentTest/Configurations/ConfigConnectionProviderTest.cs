using BlogDoFt.QueueComponent.Configurations;
using BlogDoFt.QueueComponent.Enums;
using BlogDoFt.QueueComponentTest.Fixtures;
using FluentAssertions;
using System;
using Xunit;

namespace BlogDoFt.QueueComponentTest.Configurations
{
    public class ConfigConnectionProviderTest
    {
        [Fact]
        public void ShouldBeSingleton()
        {
            // Given
            var oldInstance = ConfigConnectionProvider.NewInstance();

            // When
            var newInstance = ConfigConnectionProvider.NewInstance();

            // Then
            newInstance.Should().Be(oldInstance);
        }

        [Fact]
        public void ShouldStorageAConfig()
        {
            // Given
            var config = new QueueConnConfiguration();
            var provider = ConfigConnectionProvider.NewInstance();

            // When
            Action act = () => provider.SetConfigTo<QueueStub>(config);

            // Then
            act.Should().NotThrow();
        }

        [Fact]
        public void ShouldUpdateWhenTryStorageSameConfig()
        {
            // Given
            var config = new QueueConnConfiguration { QueueMechanism = QueueMechanism.Kafka };
            var updateConfig = new QueueConnConfiguration { QueueMechanism = QueueMechanism.RabbitMq };
            var provider = ConfigConnectionProvider.NewInstance();
            provider.SetConfigTo<QueueStub>(config);

            // When
            provider.SetConfigTo<QueueStub>(updateConfig);
            var storedConfig = provider.GetConfigTo<QueueStub>();

            // Then
            storedConfig.Should().Be(updateConfig);
        }
    }
}
