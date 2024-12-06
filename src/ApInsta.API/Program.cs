using ApInsta.Application.Validators;
using ApInsta.Domain.Interfaces;
using ApInsta.Domain.Interfaces.Repository;
using ApInsta.Infrastructure;
using ApInsta.Infrastructure.Repositories;
using ApInsta.Service;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApInsta.API
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurações de Serviços
            ConfigureServices(builder);

            // Configurações do Pipeline de Middleware
            var app = builder.Build();
            ConfigureMiddleware(app);

            app.Run();
        }

        static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração do EF Core e DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registro de Repositórios e Serviços
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Configuração de Autenticação JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // Configuração de Controladores e FluentValidation
            builder.Services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>();
                });
        }

        static void ConfigureMiddleware(WebApplication app)
        {
            // Middleware de Desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middleware de Segurança
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapeamento de Controladores
            app.MapControllers();
        }
    }
}
