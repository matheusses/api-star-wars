
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Matheusses.StarWars.WebApi.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Microsoft.Extensions.Logging.Console;
using System.Reflection;
using Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb;
using Matheusses.StarWars.Domain.Interfaces.ExternalApi;
using Matheusses.StarWars.WebApi;
using Matheusses.StarWars.Domain.Model;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSqlDatabase(builder.Configuration);
builder.Services.AddMongoDB(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddHttpClient();
builder.AddSerilog(builder.Configuration, "star-wars-api");
builder.Services.AddExternalApi(builder.Configuration);

// builder.Services.AddAutoMapper(assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();


Log.Information("Starting API");
app.Run();
