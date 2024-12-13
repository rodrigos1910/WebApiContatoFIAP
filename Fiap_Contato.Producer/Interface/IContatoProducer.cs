using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap_Contato.Producer.Interface
{
    public interface IContatoProducer
    {
        Task EnviarMensagemAsync<T>(T mensagem, string QueueName);
    }
}
