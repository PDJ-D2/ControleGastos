using API.Middlewares;
using API.Validators.Pessoa;
using Application.Interfaces;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPessoaRepository, PessoaRepo>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepo>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepo>();
builder.Services.AddScoped<ListarPessoasService>();
builder.Services.AddScoped<ListarCategoriasService>();
builder.Services.AddScoped<ListarTransacoesService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarPessoaRequestValidator>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=database.db"));
builder.Services.AddScoped<TotaisPorPessoaService>();
builder.Services.AddScoped<CriarPessoaService>();
builder.Services.AddScoped<DeletarPessoaService>();
builder.Services.AddScoped<ListarPessoasService>();

builder.Services.AddScoped<CriarCategoriaService>();
builder.Services.AddScoped<ListarCategoriasService>();

builder.Services.AddScoped<CriarTransacaoService>();
builder.Services.AddScoped<ListarTransacoesService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
