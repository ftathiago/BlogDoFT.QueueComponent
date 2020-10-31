using BlogDoFt.QueueComponent.Configurations;
using BlogDoFt.QueueComponent.Enums;
using BlogDoFt.QueueComponent.Extensions;
using BlogDoFt.QueueComponentTest.Fixtures;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BlogDoFt.QueueComponentTest.Extensions
{
    public class ConfigurationExtensionTest
    {
        private readonly ServiceCollection _services;
        private readonly Faker _faker;

        public ConfigurationExtensionTest()
        {
            _services = new ServiceCollection();
            _faker = FakerFixture.GetFaker();
        }

        [Fact]
        public void ShouldAddAConnectionToSomeClass()
        {
            _services.AddQueueSupport();
            var expectedQueueName = _faker.Lorem.Word();

            _services.RegisterConnectionTo<QueueStub>((config) =>
                config.QueueName = expectedQueueName);

            using var provider = _services.BuildServiceProvider();
            var prov = provider.GetRequiredService<ConfigConnectionProvider>();
            prov.Should().NotBeNull();
            prov.GetConfigTo<QueueStub>().QueueName.Should().Be(expectedQueueName);
        }

        [Fact]
        public void ShouldReAddAConnectionOnlyChangingFields()
        {
            _services.AddQueueSupport();
            var expectedQueueName = _faker.Lorem.Word();
            const QueueMechanism ExpectedQueueMechanism = QueueMechanism.Kafka;
            _services.RegisterConnectionTo<QueueStub>((config) =>
            {
                config.QueueName = expectedQueueName;
                config.QueueMechanism = QueueMechanism.RabbitMq;
            });

            _services.RegisterConnectionTo<QueueStub>((config) => config.QueueMechanism = ExpectedQueueMechanism);

            using var provider = _services.BuildServiceProvider();
            var prov = provider.GetRequiredService<ConfigConnectionProvider>();
            prov.Should().NotBeNull();
            prov.GetConfigTo<QueueStub>().QueueName.Should().Be(expectedQueueName);
            prov.GetConfigTo<QueueStub>().QueueMechanism.Should().Be(ExpectedQueueMechanism);
        }

        [Fact]
        public void ShouldThrowExceptionWhenNoQueueSupportAddedd()
        {
            // Given
            // _services.AddQueueSupport();

            // When
            Action act = () => _services.RegisterConnectionTo<QueueStub>((config) =>
                     config.QueueMechanism = QueueMechanism.Kafka);

            // Then
            act.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
