namespace BlogDoFt.QueueComponent.Enums
{
    public enum QueueMechanism
    {
        /// <summary>
        /// Unknown queue mechanism. May thrown exceptions.
        /// </summary>
        Unknown,

        /// <summary>
        /// Kafka.
        /// </summary>
        Kafka,

        /// <summary>
        /// RabbitMQ.
        /// </summary>
        RabbitMq,
    }
}
