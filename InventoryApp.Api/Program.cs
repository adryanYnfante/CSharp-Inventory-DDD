using InventoryApp.Api;
using InventoryApp.Domain.Configuration;
using InventoryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var MyAllowOrigins = "_myAllowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.CorsConfig(MyAllowOrigins);

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<InventoryDapperContext>();

var sqlConnection = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.DbContextConfig(sqlConnection);

builder.Services.RegisterUseCase();
builder.Services.RegisterRepositories();

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();