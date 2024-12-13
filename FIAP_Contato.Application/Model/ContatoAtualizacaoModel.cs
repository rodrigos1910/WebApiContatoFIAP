using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Contato.Application.Model
{
    public class ContatoAtualizacaoModel
    {
        public int Id { get; set; }
        public ContatoModel Contato { get; set; }

        public ContatoAtualizacaoModel(int id, ContatoModel contato)
        {
            Id = id;
            Contato = contato;
        }
    }
}
