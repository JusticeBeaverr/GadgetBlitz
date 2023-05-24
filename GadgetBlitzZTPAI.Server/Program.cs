using GadgetBlitzZTPAI.Server.Application.Commands;
using GadgetBlitzZTPAI.Server.Application.Queries;
using GadgetBlitzZTPAI.Server.Core;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using GadgetBlitzZTPAI.Server.Infrastructure.DatabaseSettings;
using GadgetBlitzZTPAI.Server.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmartphonesDatabaseSettings>(
    builder.Configuration.GetSection("SmartphonesDatabase"));
builder.Services.Configure<UsersDatabaseSettings>(
    builder.Configuration.GetSection("SmartphonesDatabase"));

builder.Services.AddSingleton<ISmartphoneRepository, SmartphoneRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(typeof(AddSmartphoneCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetAllSmartphonesQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeleteSmartphoneCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetSmartphoneByIDQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(AuthenticateUserCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.Authority = "https://localhost:5001";
        o.TokenValidationParameters.ValidateAudience = false;
        o.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
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


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins("https://localhost:7086")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "gadgetblitz/swagger/{documentname}/swagger.json";
    });


    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/gadgetblitz/swagger/v1/swagger.json", "gadgetblitz API V1");
        c.RoutePrefix = "gadgetblitz/swagger";
    });
}
app.UseCors(builder => // Dodajemy konfigurację CORS
{
    builder.WithOrigins("https://localhost:7086")
        .AllowAnyMethod()
        .AllowAnyHeader();
});
//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
