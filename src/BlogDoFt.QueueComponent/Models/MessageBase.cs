using BlogDoFt.QueueComponent.Models;
using System;
using System.Collections.Generic;

namespace BlogDoFt.QueueComponent
{
    public abstract class MessageBase : ValidatableModel
    {
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public long CreationTimeStamp { get; private set; } = DateTimeOffset.Now.ToFileTime();

        public string Payload { get; private set; }

        internal void SetPayload(string payload) => Payload = payload;

        internal void SetCreationTimeStamp(DateTimeOffset creationTimeStamp) =>
            CreationTimeStamp = creationTimeStamp.ToFileTime();
    }
}
