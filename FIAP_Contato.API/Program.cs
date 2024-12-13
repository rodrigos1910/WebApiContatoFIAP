using FIAP_Contato.API.Middleware;
using FIAP_Contato.CrossCutting;
using FIAP_Contato.CrossCutting.Logger;

using Microsoft.OpenApi.Models;

using Prometheus;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => {
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP - Contato",
        Description = "Gest�o de Contatos",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Configuration IoC
builder.Services.AddRegisterServices();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(
    new CustomLoggerProvider(
        new CustomLoggerProviderConfiguration{ LogLevel = LogLevel.Information  }
        ));

var app = builder.Build();

app.UseMetricServer();

//Metricas Prometheus
app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseResponseHandleMiddleware();

app.Run();
