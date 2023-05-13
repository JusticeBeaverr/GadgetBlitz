using GadgetBlitzZTPAI.Server.Application.Commands;
using GadgetBlitzZTPAI.Server.Application.Queries;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using GadgetBlitzZTPAI.Server.Infrastructure.DatabaseSettings;
using GadgetBlitzZTPAI.Server.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmartphonesDatabaseSettings>(
    builder.Configuration.GetSection("SmartphonesDatabase"));

builder.Services.AddSingleton<ISmartphoneRepository, SmartphoneRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(typeof(AddSmartphoneCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetAllSmartphonesQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeleteSmartphoneCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetSmartphoneByIDQueryHandler).GetTypeInfo().Assembly);

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "GadgetBlitzApi",

    });



});
builder.Services.AddSwaggerGen();

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

app.Run();
