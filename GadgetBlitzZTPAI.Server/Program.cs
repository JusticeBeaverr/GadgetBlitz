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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;


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
builder.Services.AddMediatR(typeof(ChangePasswordCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GadgetBlitzApi",
    });

    
});


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
app.UseCors(builder => // Dodajemy konfiguracjê CORS
{
    builder.WithOrigins("https://localhost:7086")
        .AllowAnyMethod()
        .AllowAnyHeader();
});
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
