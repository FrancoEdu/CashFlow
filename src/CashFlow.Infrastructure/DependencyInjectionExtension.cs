﻿using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.Repositories.Expenses;
using CashFlow.Infrastructure.Repositories.User;
using CashFlow.Infrastructure.Security.Criptographys;
using CashFlow.Infrastructure.Security.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddToken(services, configuration);
        AddAuthentication(services, configuration);
    }

    #region Private Methods

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpenseReadOnlyRepository, ExpenseRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpenseRepository>();
        services.AddScoped<IExpenseUpdateOnlyRepository, ExpenseRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordEncripter, Criptography>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CashFlowDbContext>(options => 
            options.UseMySql(
                configuration.GetConnectionString("AppConnection"), 
                new MySqlServerVersion(new Version(8, 0, 34))
                )
            );
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signinKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");

        services.AddScoped<ITokenGenerator>(config => new TokenGenerator(signinKey!, expirationTimeMinutes));
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var signinKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(signinKey!)),
                ClockSkew = TimeSpan.Zero
            };
        });
    }
    #endregion Private Methods
}
