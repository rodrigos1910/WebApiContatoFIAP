using FIAP_Contato.Application.Model;

namespace FIAP_Contato.Application.Interface;

public interface IContatoApplicationService
{
    Task  CadastrarContato(ContatoModel request);
    Task  AtualizarContato(int id, ContatoModel request);
    Task  DeletarContato(int id);
}