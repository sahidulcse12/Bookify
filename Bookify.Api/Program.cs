using Bookify.Api.Extensions;
using Bookify.Application;
using Bookify.Infrustructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrustructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.ApplyMigrations();

    app.UseSwaggerUI();

    app.SeedData();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
