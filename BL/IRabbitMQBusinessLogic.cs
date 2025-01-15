namespace TestRMQ.BL
{
    public interface IRabbitMQBusinessLogic
    {
        bool createQueue(string queueName);
        bool deleteQueue(string queueName);
        bool publish(string messages);
        List<string> getMessages(string queueName, int maxMessages);
    }
}
