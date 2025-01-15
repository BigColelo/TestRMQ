using TestRMQ.DAO;

namespace TestRMQ.BL
{
    public class RabbitMQBusinessLogic : IRabbitMQBusinessLogic
    {
        private readonly IRabbitMQDAO _rabbitMQDAO;

        public RabbitMQBusinessLogic(IRabbitMQDAO rabbitmq)
        {
            _rabbitMQDAO = rabbitmq;
        }

        public bool createQueue(string queueName)
        {
            return _rabbitMQDAO.createQueue(queueName);
        }

        public bool deleteQueue(string queueName)
        {
            return _rabbitMQDAO.deleteQueue(queueName);
        }

        public bool publish(string message)
        {
            return _rabbitMQDAO.publish(message);
        }

        public List<string> getMessages(string queueName, int maxMessages) 
        {
            return _rabbitMQDAO.getMessages(queueName, maxMessages);
        }
    }
}
