using Bogus;

using FIAP_Contato.API.Controllers;
using FIAP_Contato.Application.Model;
using FIAP_Contato.Application.Service;
using Microsoft.AspNetCore.Mvc;

using Moq;

using System.ComponentModel.DataAnnotations;

using Xunit;

namespace FIAP_Contato.Test.Unit;

public class ContatoController
{
    private readonly Faker<ContatoModel> _faker;

    public ContatoController()
    {
        _faker = new Faker<ContatoModel>("pt_BR")
            .RuleFor(f => f.Nome, f => f.Name.FullName())
            .RuleFor(f => f.Telefone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(f => f.Email, f => f.Internet.Email());
    }

    
}
