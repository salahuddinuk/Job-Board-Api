using JobBoardAPI.MQ;
using RabbitMQ.Client;
using System.Text;

namespace JobBoardAPI.MQSender
{
    public class MqSender : IMqSender
    {   
        private readonly IConfiguration _configuration;
        private readonly ILogger<MqSender> _logger;
        public MqSender(IConfiguration configuration, ILogger<MqSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration.GetSection("RabbitMq").GetSection("HostName").Value, //"localhost",    // Replace with your RabbitMQ server's hostname
                    Port = int.Parse(_configuration.GetSection("RabbitMq").GetSection("Port").Value), //5672,               // Default RabbitMQ port
                    UserName = _configuration.GetSection("RabbitMq").GetSection("UserName").Value, //"guest",        // RabbitMQ username
                    Password = _configuration.GetSection("RabbitMq").GetSection("Password").Value //"guest"         // RabbitMQ password
                };

                // Create a connection to the RabbitMQ server
                IConnection conn = await factory.CreateConnectionAsync();

                // Create a channel
                //using var channel = connection.CreateModel();
                IChannel channel = await conn.CreateChannelAsync();

                // Declare a queue
                string queueName = "myQueue";  // Replace with your queue name
                await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Message to send                
                byte[] body = Encoding.UTF8.GetBytes(message);

                // Publish the message to the queue
                var props = new BasicProperties();
                var routingKey = "demo-routing-key";
                await channel.BasicPublishAsync("DemoExchange", routingKey, false, props, body);
                //channel.BasicPublishAsync(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                //Console.WriteLine($"Sent: {message}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on Sender > "+ ex.Message);
                return false;
            }
        }

    }
}
