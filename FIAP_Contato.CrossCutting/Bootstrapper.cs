using Fiap_Contato.Producer.Interface;
using Fiap_Contato.Producer.Producer;

using FIAP_Contato.Application.Interface; 
using FIAP_Contato.Application.Service; 
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_Contato.CrossCutting;

public static class Bootstrapper
{
    public static IServiceCollection AddRegisterServices(this IServiceCollection services)
    { 
        services.AddScoped<IContatoApplicationService, ContatoApplicationService>(); 
        services.AddScoped<IContatoProducer, ContatoProducer>();

        return services;
    }
}