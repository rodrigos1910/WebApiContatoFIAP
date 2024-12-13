 
using Fiap_Contato.Producer.Interface;

using FIAP_Contato.Application.Interface;
using FIAP_Contato.Application.Model; 
using System.Threading.Tasks;

namespace FIAP_Contato.Application.Service;

public class ContatoApplicationService : IContatoApplicationService
{
   
    private readonly IContatoProducer _producer;

    public ContatoApplicationService(IContatoProducer producer)
    {
     
        _producer = producer;
    }

    public async Task AtualizarContato(int id, ContatoModel request)
    {
        var contatoAtualizao = new ContatoAtualizacaoModel(id, request);

        // Envia para a fila de atualização
        await _producer.EnviarMensagemAsync(contatoAtualizao, "fila_atualizacao");

    }

    public async Task CadastrarContato(ContatoModel request)
    {
          // Envia para a fila de cadastro
        await _producer.EnviarMensagemAsync(request, "fila_cadastro");
         
    }

    public async Task DeletarContato(int id)
    {
        // Envia o ID para a fila de exclusão
        await  _producer.EnviarMensagemAsync(id, "fila_exclusao");
         
    }
}
