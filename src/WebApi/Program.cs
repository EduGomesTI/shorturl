using Application;
using Infrastructure;
using Infrastructure.Options;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UsePresentation();

app.UseHttpsRedirection();

app.Run();