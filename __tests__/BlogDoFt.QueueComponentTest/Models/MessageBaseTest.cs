using BlogDoFt.QueueComponentTest.Fixtures;
using Bogus;
using FluentAssertions;
using System;
using System.Text.Json;
using Xunit;

namespace BlogDoFt.QueueComponentTest.Models
{
    public class MessageBaseTest
    {
        private readonly Faker _faker;

        public MessageBaseTest() =>
            _faker = FakerFixture.GetFaker();

        [Fact]
        public void ShouldBeValidWhenMessageIsValid()
        {
            // Given
            var message = GetValidMessage();

            // When
            var valid = message.IsValid();

            // Then
            valid.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeInvalidWhenMessageIsInvalid()
        {
            // Given
            var message = GetValidMessage();
            message.RequiredField = string.Empty;

            // When
            var valid = message.IsValid();

            // Then
            valid.Should().BeFalse();
        }

        [Fact]
        public void ShouldDefinePayload()
        {
            // Given
            var message = GetValidMessage();
            var payload = JsonSerializer.Serialize(message);

            // When
            message.SetPayload(payload);

            // Then
            message.Payload.Should().Be(payload);
        }

        [Fact]
        public void ShouldDefineCreationTimestamp()
        {
            // Given
            var message = GetValidMessage();
            var dateTime = DateTimeOffset.Now;
            var expectedTimestamp = dateTime.ToFileTime();

            // When
            message.SetCreationTimeStamp(dateTime);

            // Then
            message.CreationTimeStamp.Should().Be(expectedTimestamp);
        }

        private MessageStub GetValidMessage() => new MessageStub
        {
            NonRequiredField = _faker.Random.Int(1),
            RequiredField = _faker.Lorem.Sentence(),
        };
    }
}
