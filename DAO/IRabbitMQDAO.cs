namespace TestRMQ.DAO
{
    public interface IRabbitMQDAO
    {
        public bool createQueue(string queueName);
        public bool deleteQueue(string queueName);
        public bool publish(string message);
        public List<string> getMessages(string queueName, int maxMessages);
    }
}
