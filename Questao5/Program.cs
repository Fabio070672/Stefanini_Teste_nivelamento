using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Repositories.Commands;
using Questao5.Infrastructure.Repositories.Queries;
using Questao5.Infrastructure.Sqlite;
using Questao5.Presentation.Validators;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddFluentValidation(config=> config.RegisterValidatorsFromAssemblyContaining<AccountValidator>());


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Dependencies
builder.Services.AddTransient<IConsultHandler, ConsultHandler>();
builder.Services.AddTransient<IMovimentHandler, MovimentHandler>();

//Repositories
builder.Services.AddTransient<IAccountQueriesRepository, AccountQueriesRepository>();
builder.Services.AddTransient<IAccountCommandsRepository, AccountCommandsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Questão 5 - Conta Bancaria.Api",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


