 
using System.Text;
using System.Text.Json;

using Fiap_Contato.Producer.Interface;

using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;

namespace Fiap_Contato.Producer.Producer
{

    
    public class ContatoProducer: IContatoProducer
    {

        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        public ContatoProducer(IConfiguration configuration)
        {
            var rabbitConfig = configuration.GetSection("RabbitMQ");
            _hostName = rabbitConfig["HostName"];
            _userName = rabbitConfig["UserName"];
            _password = rabbitConfig["Password"];
        }


        public async Task EnviarMensagemAsync<T>(T mensagem, string QueueName)
        {

            var factory = new ConnectionFactory()
            {
                HostName = _hostName, // Nome do contêiner RabbitMQ
                UserName = _userName,  // Usuário padrão do RabbitMQ
                Password = _password   // Senha padrão do RabbitMQ
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // Declara a fila (idempotente: cria se não existir)
            await  channel.QueueDeclareAsync(queue: QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Serializa a mensagem em JSON
            var mensagemJson = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(mensagemJson);

            // Publica a mensagem na fila
            await  channel.BasicPublishAsync(exchange: "",
                                 routingKey: QueueName,
                                 body: body);

            Console.WriteLine($"Mensagem publicada na fila {QueueName}: {mensagemJson}");
        }

    }
}
