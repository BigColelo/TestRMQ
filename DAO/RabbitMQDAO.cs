using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using TestRMQ.DAO;

namespace TestRMQ.DAO
{
    public class RabbitMQDAO : IRabbitMQDAO
    {
        private IConnection connection;
        private IModel channel;
        private string Exchange_Messages = "message";

        public RabbitMQDAO(IConfiguration configuration)
        {
            var host = configuration["RabbitMQ:HostName"];
            var port = configuration["RabbitMQ:Port"];
            var user = configuration["RabbitMQ:UserName"];
            var pass = configuration["RabbitMQ:Password"];

            var factory = new ConnectionFactory
            {
                HostName = host,
                Port = int.Parse(port),
                UserName = user,
                Password = pass
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: Exchange_Messages, type: ExchangeType.Fanout);
            
        }

        public bool createQueue(string queueName)
        {
            var result = channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            if (result != null)
            {
                channel.QueueBind(queue: queueName, exchange: Exchange_Messages, routingKey: "");
                return true;
            }

            return false;
        }

        public bool deleteQueue(string queueName)
        {
            var result = channel.QueueDeclarePassive(queueName);
            if (result != null)
            {
                channel.QueueDelete(queue: queueName);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool publish(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: Exchange_Messages,
                    routingKey: "",
                    basicProperties: null,
                    body: body
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> getMessages(string queueName, int maxMessages)
        {
            List<string> messages = new List<string>();

            for (int i = 0; i < maxMessages; i++)
            {
                // Recupera un singolo messaggio dalla coda
                var result = channel.BasicGet(queueName, autoAck: true);

                if (result != null)
                {
                    // Decodifica il messaggio in una stringa
                    var message = Encoding.UTF8.GetString(result.Body.ToArray());
                    messages.Add(message);
                }
                else
                {
                    // Se la coda è vuota, esci dal ciclo
                    break;
                }
            }

            return messages;
        }
    }
}
