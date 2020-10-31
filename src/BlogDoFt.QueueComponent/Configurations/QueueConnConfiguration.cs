using BlogDoFt.QueueComponent.Enums;
using System.Collections.Generic;

namespace BlogDoFt.QueueComponent.Configurations
{
    public class QueueConnConfiguration
    {
        public QueueConnConfiguration()
        {
            CustomParameters = new Dictionary<string, string>();
            QueueMechanism = QueueMechanism.Unknown;
        }

        public string HostName { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public int RetryCount { get; set; }

        public int TimeoutMs { get; set; }

        public string QueueName { get; set; }

        public QueueMechanism QueueMechanism { get; set; }

        public Dictionary<string, string> CustomParameters { get; set; }
    }
}
